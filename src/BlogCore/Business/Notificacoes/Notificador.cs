using BlogCore.Business.Interfaces;

namespace BlogCore.Business.Notificacoes;
public class Notificador : INotificador
{
    private readonly List<Notificacao> _notificacoes = [];
    public bool TemNotificacao()
    {
        return _notificacoes.Any();
    }

    public List<Notificacao> ObterNotificacoes()
    {
        return _notificacoes;
    }

    public void AdicionarNotificacao(Notificacao notificacao)
    {
        _notificacoes.Add(notificacao);
    }
}