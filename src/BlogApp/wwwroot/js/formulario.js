$(document).ready(function () {
    const form = $('#form-comentario');
    if (form.length) {
        form.on('submit', function (event) {
            event.preventDefault();
            const formData = form.serialize();
            const postId = form.find('input[name="PostId"]').val();

            $.post(`/posts/${postId}/comentarios/novo`, formData)
                .done(function (response) {
                    form.find('#Conteudo').val('');
                    $('#comentario-lista').html(response);
                }).fail(function (xhr) {
                    console.log(xhr);
                });
        });
    }
    const modal = $('#comentario-modal');
    if (modal.length) {
        modal.on('show.bs.modal', function (event) {
            const button = $(event.relatedTarget);
            const comentarioId = button.data('id');
            const postId = button.data('postid');

            $.get(`/posts/${postId}/comentarios/detalhes/${comentarioId}`,
                function (data) {
                    modal.find('.modal-body').html(data);
                    const formModal = modal.find('#form-comentario');
                    if (formModal) {
                        formModal.on('submit', function (event) {
                            event.preventDefault();
                            const formData = formModal.serialize();

                            $.post(`/posts/${postId}/comentarios/editar/${comentarioId}`, formData)
                                .done(function (response) {
                                    $('#comentario-lista').html(response);
                                    modal.modal('hide');
                                }).fail(function (xhr) {
                                    console.log(xhr);
                                    modal.find('.modal-body').html(xhr.responseText);
                                });
                        });
                    }
                });
        });
    };
    const modalDelete = $('#comentario-modal-delete'); 
    modalDelete.on('show.bs.modal', function (event) {
        const button = $(event.relatedTarget);
        modalDelete.data('button', button);
        $('#btn-confirm').on('click', function (event) {
            event.preventDefault();

            const comentarioId = button.data('id');
            const postId = button.data('postid');

            $.post(`/posts/${postId}/comentarios/excluir/${comentarioId}`)
                .done(function (response) {
                    $('#comentario-lista').html(response);
                    modalDelete.modal('hide');
                }).fail(function (xhr) {
                    console.log(xhr);
                    modalDelete.find('.modal-body').html(xhr.responseText);
                });
        });
    });

});
