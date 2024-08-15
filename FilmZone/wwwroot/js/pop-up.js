document.addEventListener('DOMContentLoaded', function () {
    const openPopUp = document.getElementById('open_pop_up');
    const closePopUp = document.getElementById('pop_up_close');
    const popUp = document.getElementById('pop_up');
    /*const popUp = document.getElementById('pop_up');*/
    if (openPopUp != null) {
        openPopUp.addEventListener('click', function (e) {
            e.preventDefault();
            popUp.classList.add('active');
            document.body.classList.add("no-scroll");
        })

        closePopUp.addEventListener('click', () => {
            popUp.classList.remove('active');
            document.body.classList.remove("no-scroll");
        })
    }
});
