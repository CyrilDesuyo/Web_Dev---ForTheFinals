const wrapper = document.querySelector('.wrapper');
const registerLink = document.querySelector('.register-link');
const loginLink = document.querySelector('.login-link');

registerLink.onclick = () => {
    wrapper.classList.add('active');
}

loginLink.onclick = () => {
    wrapper.classList.remove('active');
}

function toggleSelect() {
    const wrapper = document.querySelector('.wrapper');
    wrapper.classList.toggle('active');
}

const selectButton = document.querySelector('.select-button');
selectButton.addEventListener('click', toggleSelect);
