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

$(".profile .icon_wrap").click(function(){
    $(this).parent().toggleClass("active");
    $(".notifications").removeClass("active");
});

$(".notifications .icon_wrap").click(function(){
    $(this).parent().toggleClass("active");
    $(".profile").removeClass("active");
});

$(".show_all .link").click(function(){
     $(".notifications").removeClass("active");
     $(".popup").show();
     $(".card").css("z-index", "-2");
});

$(".close, .shadow").click(function(){
    $(".popup").hide();
    $(".card").css("z-index", "1000");
});