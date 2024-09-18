using BlogCore.Business.Interfaces;

namespace BlogCore.Business.Notificacoes
{
    public class Notificador() : INotificador
    {
        private readonly List<Notificacao> _notificacaos = [];

        public IEnumerable<Notificacao> ObterNotificacoes() 
        {
            return _notificacaos;
        }
        public void Adicionar(Notificacao notificacao)
        {
            _notificacaos.Add(notificacao);
        }

        public bool TemNotificacao()
        {
            return _notificacaos.Count != 0;
        }

    }
}
