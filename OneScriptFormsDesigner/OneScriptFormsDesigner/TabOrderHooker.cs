using System;
using System.ComponentModel.Design;
using System.Reflection;
using System.Diagnostics;

namespace osfDesigner
{
    public class TabOrderHooker
    {
        private const string _Name_ = "TabOrderHooker";
        public object _tabOrder = null;

        // Включает/отключает порядок обхода.
        public void HookTabOrder( IDesignerHost host )
        {
            const string _signature_ = _Name_ + @"::ctor()";

            // порядок обхода должен быть вызван ПОСЛЕ загрузки поверхности проектирования, поэтому мы делаем небольшую проверку
            if ( null == host.RootComponent )
                throw new Exception( _signature_ + " - Exception: the TabOrder must be invoked after the DesignSurface has been loaded!" );

            try
            {
                System.Reflection.Assembly designAssembly = System.Reflection.Assembly.Load ( "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" );
                Type tabOrderType = designAssembly.GetType( "System.Windows.Forms.Design.TabOrder" );
                if ( _tabOrder == null )
                {
                    // вызов конструктора
                    _tabOrder = Activator.CreateInstance( tabOrderType, new object[] { host } );
                }
                else
                {
                    DisposeTabOrder();
                }
            }
            catch( Exception exx )
            {
                Debug.WriteLine( exx.Message);
                if( null != exx.InnerException)
                {
                    Debug.WriteLine( exx.InnerException.Message);
                }
                throw;
            }
        }

        // управление порядком обхода
        public void DisposeTabOrder()
        {
            if ( null == _tabOrder ) return;
            try
            {
                System.Reflection.Assembly designAssembly = System.Reflection.Assembly.Load ( "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" );
                Type tabOrderType = designAssembly.GetType ( "System.Windows.Forms.Design.TabOrder" );
                tabOrderType.InvokeMember ( "Dispose", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, _tabOrder, new object[] { true } );
                _tabOrder = null;
            }
            catch( Exception exx )
            {
                Debug.WriteLine( exx.Message);
                if( null != exx.InnerException)
                {
                    Debug.WriteLine( exx.InnerException.Message);
                }
                throw;
            }
        }
    }
}
