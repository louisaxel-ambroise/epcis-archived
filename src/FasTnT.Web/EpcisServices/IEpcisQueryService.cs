using System.ServiceModel;

namespace FasTnT.Web.EpcisServices
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract(Name = "EpcisQuery", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public interface IEpcisQueryService
    {
        [OperationContract(Name = "getQueryNames")]
        [return: MessageParameter(Name = "QueryNamesResult")]
        string[] GetQueryNames([MessageParameter(Name = "QueryNamesRequest")] EmptyParms request);

        [OperationContract(Name = "poll")]
        PollResponse Query(PollRequest pollRequest);
    }
}
