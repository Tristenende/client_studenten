document.addEventListener('DOMContentLoaded', function() {
    const sendInformation = document.getElementById('sendForm');

    sendInformation.addEventListener('click', async function() {
        incorrectFields.length = 0;

        if(isFieldCorrect()) {
            sendMail();
            clearFormFields();
            clearFlashMessages();
        } else {
            flashIncorrect();
        }

    });
});

async function sendMail() {
    const mailContent = getContactFields();

    try {
        let response = await fetch('http://localhost:5136/Mail', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(mailContent)
        });
        //let data = await response.json(); //DE ENE KEER WEL NODIG ANDERE KEER NIET????
        flashCorrect();
        console.log(response);
    } catch(error) {
        flashIncorrectSend();
        console.log(error);
    }

}

let incorrectFields = [];

function getContactFields() {
    const contactFields = {
        firstName: document.getElementById('firstNameInput').value,
        lastName: document.getElementById('lastNameInput').value,
        phoneNumber: document.getElementById('phoneNumberInput').value,
        email: document.getElementById('emailInput').value,
        subject: document.getElementById('subjectInput').value,
        body: document.getElementById('messageInput').value
    };
    return contactFields;
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