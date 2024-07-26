const heart = document.getElementById('heart');
heart.addEventListener('click', function (e) {
    e.preventDefault();
    heart.classList.add('focus');
})