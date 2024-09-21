using BlogCore.Business.Notificacoes;

namespace BlogCore.Business.Interfaces
{
    public interface INotificador
    {
        void Adicionar(Notificacao notificacao);
        bool TemNotificacao();
        IEnumerable<Notificacao> ObterNotificacoes();
        bool ContemNotificacao(string mensagem);
    }
}
