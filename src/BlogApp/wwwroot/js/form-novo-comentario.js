$(document).ready(function (){
    $('#FormNovoComentario').submit(function (event) {
        event.preventDefault();
        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: $(this).serialize(),
            success: function(result) {
                $('#SectionComentario').html(result);
                $(this).reset();
            },
            error: function(xhr) {
                if (xhr.status === 401) {
                    window.location.href = '/Identity/Account/Login';
                } else {
                    $('#errors').html(xhr.responseText);
                }
            }
        });
    });
});
