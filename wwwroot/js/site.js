function checkLogin(log, popUp){
    console.log(log);
    switch (log){
        case -2:
            console.log("Ese nobre de usuario ya existe :(");
            Swal.fire({
                title: 'Error :(',
                text: 'El usuario ingresado ya existe',
                icon: 'warning',
            });
            break;
        case -1:
            console.log("Ese usuario no existe :(");
            Swal.fire({
                title: 'Error :(',
                text: 'El usuario ingresado no existe',
                icon: 'question',
            });
            break;
        case 0:
            console.log("Contraseña incorrecta :(");
            Swal.fire({
                title: 'Error :(',
                text: 'La contraseña ingresada es incorrecta',
                icon: 'error',
            });
            break;
        case 1:
            console.log("Logeado correctamente :)");
            Swal.fire({
                title: 'Perfe :)',
                text: 'Te has logeado correctamente',
                icon: 'success',
            });
            break;
        default:
            console.log("Default del switch - ViewBag.Log")
            break;
    }

}