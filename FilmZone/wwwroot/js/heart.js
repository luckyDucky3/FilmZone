//const heart = document.getElementById('heart');
//heart.addEventListener('click', function (e) {
//    e.preventDefault();
//    heart.classList.toggle('focus');
//    const focus = document.getElementById('focus');
//    if (focus != null) {
//        fetch('/api/api/addFilm', {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify({ name: @Html.Raw(JsonSerializer.Serialize(Model.Name)) }),
//        })
//        .then(response => response.json())
//        .then(data => {
//            if (data.success)
//                console.log('C# метод addFilm успешно вызван');
//            else
//                console.log('Произошла ошибка при вызове C# addFilm метода');
//        })
//        .catch(error => {
//            console.error('Ошибка:', error);
//        });
//    }
//    else {
//        fetch('/api/api/removeFilm', {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//        })
//        .then(response => response.json())
//        .then(data => {
//            if (data.success)
//                console.log('C# метод removeFilm успешно вызван');
//            else
//                console.log('Произошла ошибка при вызове C# removeFilm метода');
//        })
//        .catch(error => {
//            console.error('Ошибка:', error);
//        });
//    }
//});