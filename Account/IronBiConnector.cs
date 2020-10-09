using System;
using System.Diagnostics;
using Autofac;

namespace SSAPIInteractionHelper.Account
{
    public class IronAccessorControl
    {
        private  static  ContainerBuilder builder = new ContainerBuilder();
        public static IContainer Container;

        static IronAccessorControl()
        {
            IronAccess();
        }
        public static void IronAccess()
        {
            try
            {
                builder.RegisterType<IronOrderAccess>().As<IIronOrderAccess>().InstancePerLifetimeScope();
                builder.RegisterType<IronAccount>().As<IIronAccount>().InstancePerLifetimeScope();
                builder.RegisterType<IronFinancials>().As<IIronFinancials>().InstancePerLifetimeScope();
                Container = builder.Build();
            }
            catch (Exception e)
            {
                Debug.Write(e);
                throw;
            }
        }
    }
}
