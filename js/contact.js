document.addEventListener('DOMContentLoaded', function() {
    const sendInformation = document.getElementById('sendForm');

    sendInformation.addEventListener('click', async function() {

        showLoadingScreen()
        incorrectFields.length = 0;

        if(isFieldCorrect()) {
            sendMail();
            clearFormFields();
            clearFlashMessages();
            hideLoadingScreen()
        } else {
            flashIncorrect();
            hideLoadingScreen()
        }

    });
});

async function sendMail() {
    const mailContent = getContactFields();


    try {
        let response = await fetch('https://localhost:7099/Mail', {
            method: 'POST',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(mailContent)
        });

        if(response.status == 200){
            flashCorrect();
        } else{
            flashError()
        }
        console.log(response);
    } catch(error) {
        flashIncorrectSend();
        console.log(error);
        console.log(mailContent)
    }

}

let incorrectFields = [];

function getContactFields() {
    return {
        firstName: document.getElementById('firstNameInput').value,
        lastName: document.getElementById('lastNameInput').value,
        phoneNumber: document.getElementById('phoneNumberInput').value,
        email: document.getElementById('emailInput').value,
        subject: document.getElementById('subjectInput').value,
        body: document.getElementById('messageInput').value
    };
}

function isFieldCorrect() {
    const input = getContactFields();

    for(let fields in input) {
        if (input[fields] == '') {
            incorrectFields.push(fields);
        }
    }

    if (incorrectFields.length == 0) {
        return true;
    }
    return false;
}

function flashCorrect() {
    let message = 'Mail is verzonden!'

    let sendMail = document.getElementById("input-field");
    sendMail.classList.remove('input-field');

    sendMail.textContent = message;
}

function flashIncorrect() {
    let errorMessage = "Er is iets niet ingevuld!";

    let errorDiv = document.getElementById("input-field");
    errorDiv.classList.remove('input-field');

    errorDiv.textContent = errorMessage;
}

function flashError() {
    let errorMessage = "Er is iets misgegaan op de server!";

    let errorDiv = document.getElementById("input-field");
    errorDiv.classList.remove('input-field');

    errorDiv.textContent = errorMessage;
}

function flashIncorrectSend() {
    let errorMessage = "Er is iets fout gegaan bij het versturen, probeer het later opnieuw";

    let errorDiv = document.getElementById("input-field");
    errorDiv.classList.remove('input-field');

    errorDiv.textContent = errorMessage;
}

function clearFormFields() {
    document.getElementById('firstNameInput').value = '';
    document.getElementById('lastNameInput').value = '';
    document.getElementById('phoneNumberInput').value = '';
    document.getElementById('emailInput').value = '';
    document.getElementById('subjectInput').value = '';
    document.getElementById('messageInput').value = '';
}

function clearFlashMessages() {
    let errorDiv = document.getElementById("input-field");
    errorDiv.textContent = '';
}

function showLoadingScreen() {
    document.getElementById('loadingScreen').style.display = 'flex';
}

function hideLoadingScreen() {
    document.getElementById('loadingScreen').style.display = 'none';
}


