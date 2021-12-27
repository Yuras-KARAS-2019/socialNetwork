let user;

buttons.style.display ='none';

async function sendRequest(endpoint = '', method = 'GET', data = {}) {
    const response = await fetch(`http://localhost:58655/${endpoint}`, {
        method: method,
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: method == 'POST' ? JSON.stringify(data) : undefined
    });
    if (response.ok) {
        const isJson = response.headers.get('content-type')?.includes('application/json');
        return isJson ? await response.json() : null;
    }
    return null;
}

function syntaxHighlight(data) {
    let json = JSON.stringify(data, null, 2);
    json = json.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
    return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
        let cls = 'number';
        if (/^"/.test(match)) {
            if (/:$/.test(match)) {
                cls = 'key';
            } else {
                cls = 'string';
            }
        } else if (/true|false/.test(match)) {
            cls = 'boolean';
        } else if (/null/.test(match)) {
            cls = 'null';
        }
        return '<span class="' + cls + '">' + match + '</span>';
    });
}

loginSubmit.addEventListener("click", async () => {
    const credentionals = {
        login: loginForm.login.value,
        password: loginForm.password.value
    }
    user = await sendRequest('api/users/byCreds', 'POST', credentionals);
    if(user) {
        alert(`Welcome ${user.name} ${user.lastName}`);
        loginForm.remove();
        loginSubmit.remove();
        buttons.style.display = 'block' ;
    } else {
        alert('Incorrent login or password');
    }

});


showAllUsers.addEventListener('click', async () => {
    result.innerHTML = syntaxHighlight(await sendRequest('api/users'));
});

showMySubcriptions.addEventListener('click', async () => {
    result.innerHTML = syntaxHighlight(await sendRequest(`api/Users/${user.id}/subscriptions`));
});

showMySubcribers.addEventListener('click', async () => {
    result.innerHTML = syntaxHighlight(await sendRequest(`api/Users/${user.id}/subscribers`));
});

subscribeToUser.addEventListener('click', async () => { 
    await sendRequest(`api/users/subscribe/${user.id}/${prompt('Ented user id')}`, 'POST');
    result.innerHTML = syntaxHighlight({});
});

unsubcribeFromUser.addEventListener('click', async () => {
    await sendRequest(`api/users/unsubscribe/${user.id}/${prompt('Ented user id')}`, 'POST');
    result.innerHTML = syntaxHighlight({});
});

showChatWithUser.addEventListener('click', async () => {
    result.innerHTML = syntaxHighlight(await sendRequest(`api/messages/${user.id}/${prompt('Ented user id')}`));
});

sendMessageToUser.addEventListener('click', async () => {
    result.innerHTML = syntaxHighlight(await sendRequest('api/messages', 'POST', { fromId: user.id, toId: prompt('Ented user id'), text: prompt('Ented text')}));
});

