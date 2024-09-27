$(document).ready(function () {
    $('#modal').on('show.bs.modal', function(event) {
        const button = $(event.relatedTarget);
        const { id, action } = button.data();
        const modal = $(this);
        const form = $('#FormModalComentario');

        if (dataController[action]) {
            const { url, text } = dataController[action];
            modal.find('.modal-title').text(text);
            form.attr('action', url + id);
        }
        form.find('#Id').val(id);
    });
    $('#FormModalComentario').submit(function (event) {
        event.preventDefault();
        $.ajax({
            url: $(this).attr('action'),
            method: 'POST',
            data: $(this).serialize(),

            success: function (response) {
                $('#SectionComentario').html(response);
                $(this).reset();
            },
            error: function ({ status, responseText }) {
                if (status === 401) {
                    window.location.href = '/Identity/Account/Login';
                } else {
                    $('#errors').html(responseText);
                }
            }

        })
    })
});

const dataController = {
    edit: {
        url: '/comentarios/editar/',
        text: 'Editar Comentário'
    },
    delete: {
        url: '/comentarios/excluir/',
        text: 'Excluir Comentário'
    }
};
