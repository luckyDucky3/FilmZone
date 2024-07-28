const heart = document.getElementById('heart');
heart.addEventListener('click', function (e) {
    e.preventDefault();
    heart.classList.toggle('focus');
    fetch('/api/api/heart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(data => {
        if (data.success)
            console.log('C# метод успешно вызван');
        else
            console.log('Произошла ошибка при вызове C# метода');
    })
    .catch(error => {
        console.error('Ошибка:', error);
    });
});