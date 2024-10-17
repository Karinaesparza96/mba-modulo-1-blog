$(document).ready(function () {
    function handleError({status}) {
        switch (status) {
        case 401:
            window.location.href = '/Identity/Account/Login';
            break;
        case 403:
                window.location.href = `/Error/${403}`;
            break;
        case 404:
                window.location.href = `/Error/${404}`;
            break;
        default:
            window.location.href = '/Error/';
        }
    }

    const form = $('#form-comentario');
    const modal = $('#comentario-modal');
    const modalDelete = $('#comentario-modal-delete');

    if (form.length) {
        form.on('submit', function (event) {
            event.preventDefault();

            if ($(this).valid()) {
                const formData = form.serialize();
                const postId = form.find('input[name="PostId"]').val();

                $.post(`/posts/${postId}/comentarios/novo`, formData)
                    .done(function (response) {
                        const tempDiv = $('<div>').html(response);
                        if (tempDiv.find('#comentario-lista').length) {
                            form.find('#Conteudo').val('');
                            $('#comentario-lista').html(response);
                        } else {
                            form.html(response);
                        }

                    }).fail(handleError);
            }

        });
    }

    if (modal.length) {
        modal.on('show.bs.modal', function (event) {
            const button = $(event.relatedTarget);
            const comentarioId = button.data('id');
            const postId = button.data('postid');

            function handleSubmit(event) {
                event.preventDefault();
                const formData = $(this).serialize();

                $.post(`/posts/${postId}/comentarios/editar/${comentarioId}`, formData)
                    .done(function (response) {
                        const tempDiv = $('<div>').html(response);
                        if (tempDiv.find('#comentario-lista').length) {
                            $('#comentario-lista').html(response);
                            modal.modal('hide');
                        } else {
                            modal.find('.modal-body').html(response);
                        }
                    }).fail(handleError);
            }

            $.get(`/posts/${postId}/comentarios/editar/${comentarioId}`, function (data) {
                modal.find('.modal-body').html(data);
                const formModal = modal.find('#form-comentario');
                $.validator.unobtrusive.parse(formModal);

                formModal.off('submit').on('submit', handleSubmit);

            }).fail(handleError);
        });
    };
    modalDelete.on('show.bs.modal', function (event) {
        const button = $(event.relatedTarget);
        modalDelete.data('button', button);
        $('#btn-confirm').off('click').on('click', function (event) {
            event.preventDefault();

            const comentarioId = button.data('id');
            const postId = button.data('postid');

            $.post(`/posts/${postId}/comentarios/excluir/${comentarioId}`)
                .done(function (response) {
                    $('#comentario-lista').html(response);
                    modalDelete.modal('hide');
                }).fail(handleError);
        });
    });

});
