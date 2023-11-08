function checkLogin(log, Sesion = 0){
    if (Sesion == 1){
        switch (log){
            case "-2":
                console.log("Ese nobre de usuario ya existe :(");
                Swal.fire({
                    title: 'Error :(',
                    text: 'El usuario ingresado ya existe',
                    icon: 'warning',
                });
                break;
            case "-1":
                console.log("Ese usuario no existe :(");
                Swal.fire({
                    title: 'Error :(',
                    text: 'El usuario ingresado no existe',
                    icon: 'question',
                });
                break;
            case "0":
                console.log("Contraseña incorrecta :(");
                Swal.fire({
                    title: 'Error :(',
                    text: 'La contraseña ingresada es incorrecta',
                    icon: 'error',
                });
                break;
            case "1":
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
}

function addFriend(){
    Swal.fire({
        title: "¿A quien le quiere enviar una solicitud de amistad?",
        input: "text",
        inputAttributes: {
            autocapitalize: "off",
            placeholder: "Nombre de usuario..."
        },
        showCancelButton: true,
        confirmButtonText: "Enviar",
        cancelButtonText: "Cancelar",
        showLoaderOnConfirm: true,
        preConfirm: async (nombreUsuario) => {
            $.ajax({
                type: 'POST',
                dataType: 'JSON',
                url: '/Home/EnviarSolicitudAJAX',
                data: {nombreUsuario},
                success:
                    function (response){
                        Swal.fire({
                            title: `Solicitud enviada a ${response.nombre}`,
                            imageUrl: response.fotoPerfil
                        });
                    },
                error:
                    function(){
                        Swal.fire({
                            icon: "error",
                            title: "Error",
                            text: "El usuario ingresado no existe",
                        });
                    }   
            });
        },
        allowOutsideClick: () => !Swal.isLoading()
    });
}

window.addEventListener("DOMContentLoaded", () => {
    const gameContainer = document.getElementById("gameContainer");
    const juego = document.getElementById("idJuego").value;
    switch (juego){
        case "Tetris":
            gameContainer.innerHTML = `
            <div class="TetrisContainer" id="TetrisContainer">
                <h1>Tetris</h1>
                <canvas id="TetrisCanvas"></canvas>
                <div class="TetrisBot">
                    <strong>Puntos: <span class="puntos">0</span></strong>
                </div>
            </div>
            <div class="TetrisPerdiste" id="TetrisPerdiste">
                <h1>Perdiste...</h1>
                <strong>Puntos: <span class="TetrisPuntos">0</span></strong>
                <p>Click para volver a comenzar</p>
            </div>`;
            break;
        case "Snake":
            break;
        case "2048":
            break;
        case "Wordle":
            break;
        case "Buscaminas":
            break;
        default:
            break;
    }
});