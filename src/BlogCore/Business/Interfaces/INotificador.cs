using BlogCore.Business.Notificacoes;

namespace BlogCore.Business.Interfaces;

public interface INotificador
{
    bool TemNotificacao();

    List<Notificacao> ObterNotificacoes();

    void AdicionarNotificacao(Notificacao notificacao);
}