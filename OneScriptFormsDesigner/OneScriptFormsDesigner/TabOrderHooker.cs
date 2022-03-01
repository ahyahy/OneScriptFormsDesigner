using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System;

namespace osfDesigner
{
    public class TabOrderHooker
    {
        public object _tabOrder = null;

        // Включает/отключает порядок обхода.
        public void HookTabOrder(IDesignerHost host)
        {
            // Порядок обхода должен быть вызван ПОСЛЕ загрузки поверхности проектирования, поэтому мы делаем небольшую проверку.
            if (null == host.RootComponent)
            {
                throw new Exception(@"TabOrderHooker::ctor() - Исключение: порядок вкладок должен быть вызван после загрузки поверхности дизайна!");
            }

            try
            {
                Assembly designAssembly = Assembly.Load ("System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
                Type tabOrderType = designAssembly.GetType("System.Windows.Forms.Design.TabOrder");
                if (_tabOrder == null)
                {
                    // Вызов конструктора.
                    _tabOrder = Activator.CreateInstance(tabOrderType, new object[] { host });
                }
                else
                {
                    DisposeTabOrder();
                }
            }
            catch(Exception exx)
            {
                Debug.WriteLine(exx.Message);
                if (null != exx.InnerException)
                {
                    Debug.WriteLine(exx.InnerException.Message);
                }
                throw;
            }
        }

        // Управление порядком обхода.
        public void DisposeTabOrder()
        {
            if (null == _tabOrder)
            {
                return;
            }
            try
            {
                Assembly designAssembly = Assembly.Load ("System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
                Type tabOrderType = designAssembly.GetType ("System.Windows.Forms.Design.TabOrder");
                tabOrderType.InvokeMember ("Dispose", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, _tabOrder, new object[] { true });
                _tabOrder = null;
            }
            catch(Exception exx)
            {
                Debug.WriteLine(exx.Message);
                if (null != exx.InnerException)
                {
                    Debug.WriteLine(exx.InnerException.Message);
                }
                throw;
            }
        }
    }
}
