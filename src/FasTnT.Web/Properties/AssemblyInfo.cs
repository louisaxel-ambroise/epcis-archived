using FasTnT.Web;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("FasTnT.Web")]
[assembly: AssemblyDescription("Web Management of FasTnT EPCIS server")]
[assembly: ComVisible(false)]
[assembly: Guid("c3f112b2-3e55-4481-8358-2f79b92b4fa9")]

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]
