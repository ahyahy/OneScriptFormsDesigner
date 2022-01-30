using System.Windows.Forms;

public static class ExtensionMethods
{
    public static void EnableContextMenu(this RichTextBox rtb)
    {
        if (rtb.ContextMenuStrip == null)
        {
            // Создайте контекстное меню без значков.
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.ShowImageMargin = false;

            // 1. Добавьте опцию отмены.
            ToolStripMenuItem tsmiUndo = new ToolStripMenuItem("Отменить");
            tsmiUndo.Click += (sender, e) => rtb.Undo();
            cms.Items.Add(tsmiUndo);

            // 2. Добавьте опцию Повтора.
            ToolStripMenuItem tsmiRedo = new ToolStripMenuItem("Вернуть");
            tsmiRedo.Click += (sender, e) => rtb.Redo();
            cms.Items.Add(tsmiRedo);

            // Добавьте разделитель.
            cms.Items.Add(new ToolStripSeparator());

            // 3. Добавьте опцию Вырезать (вырезает выделенный текст внутри поля richtextbox).
            ToolStripMenuItem tsmiCut = new ToolStripMenuItem("Вырезать");
            tsmiCut.Click += (sender, e) => rtb.Cut();
            cms.Items.Add(tsmiCut);

            // 4. Добавьте опцию Копирования (копирует выделенный текст в поле richtextbox).
            ToolStripMenuItem tsmiCopy = new ToolStripMenuItem("Копировать");
            tsmiCopy.Click += (sender, e) => rtb.Copy();
            cms.Items.Add(tsmiCopy);

            // 5. Добавьте опцию Вставки (добавляет текст из буфера обмена в поле richtextbox).
            ToolStripMenuItem tsmiPaste = new ToolStripMenuItem("Вставить");
            tsmiPaste.Click += (sender, e) => rtb.Paste();
            cms.Items.Add(tsmiPaste);

            // 6. Добавьте опцию Удаления (удалите выделенный текст в поле richtextbox).
            ToolStripMenuItem tsmiDelete = new ToolStripMenuItem("Удалить");
            tsmiDelete.Click += (sender, e) => rtb.SelectedText = "";
            cms.Items.Add(tsmiDelete);

            // Добавьте разделитель.
            cms.Items.Add(new ToolStripSeparator());

            // 7. Добавьте опцию <Выбрать все> (выделяет весь текст внутри поля richtextbox).
            ToolStripMenuItem tsmiSelectAll = new ToolStripMenuItem("Выбрать всё");
            tsmiSelectAll.Click += (sender, e) => rtb.SelectAll();
            cms.Items.Add(tsmiSelectAll);

            // При открытии меню проверьте, выполнено ли условие, чтобы включить действие.
            cms.Opening += (sender, e) =>
            {
                tsmiUndo.Enabled = !rtb.ReadOnly && rtb.CanUndo;
                tsmiRedo.Enabled = !rtb.ReadOnly && rtb.CanRedo;
                tsmiCut.Enabled = !rtb.ReadOnly && rtb.SelectionLength > 0;
                tsmiCopy.Enabled = rtb.SelectionLength > 0;
                tsmiPaste.Enabled = !rtb.ReadOnly && Clipboard.ContainsText();
                tsmiDelete.Enabled = !rtb.ReadOnly && rtb.SelectionLength > 0;
                tsmiSelectAll.Enabled = rtb.TextLength > 0 && rtb.SelectionLength < rtb.TextLength;
            };

            rtb.ContextMenuStrip = cms;
        }
    }
}
