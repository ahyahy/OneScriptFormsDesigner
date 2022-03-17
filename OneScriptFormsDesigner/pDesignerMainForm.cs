using osfDesigner.Properties;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class pDesignerMainForm : System.Windows.Forms.Form, IDesignerMainForm
    {
        private string _version = string.Empty;
        public pDesigner pDesignerCore = new pDesigner();
        private IpDesigner IpDesignerCore = null;
        private IContainer components = null;
        private MenuStrip menuStrip1;

        private ToolStripMenuItem _file;
        private ToolStripMenuItem _addForm;
        private ToolStripMenuItem _useSnapLines;
        private ToolStripMenuItem _useGrid;
        private ToolStripMenuItem _useGridWithoutSnapping;
        private ToolStripMenuItem _useNoGuides;
        private ToolStripMenuItem _deleteForm;
        private ToolStripSeparator _stripSeparator1;
        private ToolStripMenuItem _generateScript;

        private ToolStripSeparator _stripSeparator2;
        private ToolStripMenuItem _loadForm;
        private ToolStripMenuItem _saveForm;
        private ToolStripMenuItem _saveFormAs;
        private ToolStripSeparator _stripSeparator4;
        private ToolStripMenuItem _exit;

        private ToolStripMenuItem _edit;
        private ToolStripMenuItem _unDo;
        private ToolStripMenuItem _reDo;
        private ToolStripSeparator _stripSeparator3;
        private ToolStripMenuItem _cut;
        private ToolStripMenuItem _copy;
        private ToolStripMenuItem _paste;
        private ToolStripMenuItem _delete;

        private ToolStripMenuItem _view;
        private ToolStripMenuItem _form;
        private ToolStripMenuItem _code;

        private ToolStripMenuItem _tools;
        private ToolStripMenuItem _tabOrder;
        private static ToolStripMenuItem _tabOrder1;

        private ToolStripSeparator _stripSeparator5;
        private ToolStripMenuItem _run;

        private ToolStripSeparator _stripSeparator6;
        private ToolStripMenuItem _settings;

        private ToolStripMenuItem _help;
        private ToolStripMenuItem _about;

        private System.Windows.Forms.Panel pnl4Toolbox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel pnl4pDesigner;
        private System.Windows.Forms.Splitter pnl4splitter;

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Timer timerLoad;

        private MySettingsForm settingsForm;

        private void timerLoad_Tick(object sender, EventArgs e)
        {
            timerLoad.Stop();
            try
            {
                // Через try, потому что, при загрузке, элемент управления не завершил создание самого себя.
                Application.AddMessageFilter(new PropertyGridMessageFilter(propertyGrid1.GetChildAtPoint(new Point(40, 40)), new MouseEventHandler(propGridView_MouseUp)));
            }
            catch { }
        }

        private void propGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && (
                propertyGrid1.SelectedGridItem.Label == "СписокИзображений" ||
                propertyGrid1.SelectedGridItem.Label == "СписокБольшихИзображений" ||
                propertyGrid1.SelectedGridItem.Label == "СписокМаленькихИзображений"))
            {
                // Пользователь щелкнул левой кнопкой мыши по свойству.
                try
                {
                    propertyGrid1.SelectedGridItem.Expanded = false;
                }
                catch { }
            }
        }

        public pDesignerMainForm()
        {
            if ((bool)Settings.Default["visualSyleForms"])
            {
                Application.EnableVisualStyles();
            }
            ComponentResourceManager resources = new ComponentResourceManager(typeof(pDesignerMainForm));
            propertyGrid1 = OneScriptFormsDesigner.PropertyGrid;
            menuStrip1 = new MenuStrip();
            _file = new ToolStripMenuItem();
            _addForm = new ToolStripMenuItem();
            _useSnapLines = new ToolStripMenuItem();
            _useGrid = new ToolStripMenuItem();
            _useGridWithoutSnapping = new ToolStripMenuItem();
            _useNoGuides = new ToolStripMenuItem();
            _deleteForm = new ToolStripMenuItem();
            _stripSeparator1 = new ToolStripSeparator();
            _generateScript = new ToolStripMenuItem();
            _stripSeparator2 = new ToolStripSeparator();
            _loadForm = new ToolStripMenuItem();
            _saveForm = new ToolStripMenuItem();
            _saveFormAs = new ToolStripMenuItem();
            _stripSeparator4 = new ToolStripSeparator();
            _exit = new ToolStripMenuItem();

            _edit = new ToolStripMenuItem();
            _unDo = new ToolStripMenuItem();
            _reDo = new ToolStripMenuItem();
            _stripSeparator3 = new ToolStripSeparator();
            _cut = new ToolStripMenuItem();
            _copy = new ToolStripMenuItem();
            _paste = new ToolStripMenuItem();
            _delete = new ToolStripMenuItem();

            _view = new ToolStripMenuItem();
            _form = new ToolStripMenuItem();
            _code = new ToolStripMenuItem();

            _tools = new ToolStripMenuItem();
            _tabOrder = new ToolStripMenuItem();
            _tabOrder1 = _tabOrder;

            _stripSeparator5 = new ToolStripSeparator();
            _run = new ToolStripMenuItem();

            _stripSeparator6 = new ToolStripSeparator();
            _settings = new ToolStripMenuItem();

            _help = new ToolStripMenuItem();
            _about = new ToolStripMenuItem();
            pnl4Toolbox = new System.Windows.Forms.Panel();
            listBox1 = new System.Windows.Forms.ListBox();
            pnl4pDesigner = new System.Windows.Forms.Panel();
            pnl4splitter = new System.Windows.Forms.Splitter();
            menuStrip1.SuspendLayout();
            pnl4Toolbox.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] {
            _file,
            _edit,
            _view,
            _tools,
            _help});
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Padding = new Padding(8, 2, 0, 2);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // _file
            // 
            _file.DropDownItems.AddRange(new ToolStripItem[] {
            _addForm,
            _deleteForm,
            _stripSeparator1,
            _generateScript,
            _stripSeparator2,
            _loadForm,
            _saveForm,
            _saveFormAs,
            _stripSeparator4,
            _exit});
            _file.Name = "_file";
            _file.Size = new Size(54, 24);
            _file.Text = "Файл";
            // 
            // _addForm
            // 
            _addForm.DropDownItems.AddRange(new ToolStripItem[] {
            _useSnapLines,
            _useGrid,
            _useGridWithoutSnapping,
            _useNoGuides});
            string str_addForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABR0lEQVR42u3avQ/BQBgGcBcEia2LSESE1WK3+p/tdoNuxcBgsxJKPTc0/ZBe2lzPfXjf5Fxocvf8KqmrK2s4Ukx3AIIIIJHuMDIG/tJBu+tOI1ldDkl/Exx0052qZPU4IH7DIQHaTHcqyQo45InW0p1EskIOCdGaupNI1osghhVBTKsviG1Llvg3kCCmFEHqTHDOf4bJR1ZBouJbhgOrtu4zFnJCgDFBSkBk7x4z4xKEIIrqHyBXBPCMgUTq/lp6s+xKXR0EI6/RrRRBePkIOf8FZI9uqhCyQcilC5AtQi5cgBxZMr5SyAXdQCEkfYlWftXy0Q0LDrfR+gXHHmg7wdAeAk6yU1WDlL2cljoRNi1RhCCbIELYPyxRCEIQgggwrPr8+iH1nY8Ekt56sxUS5nd1bcJkcju1Pe3MAwNxWf8IhxNFENPqA2/lwIZlxdeeAAAAAElFTkSuQmCC";
            _addForm.Image = OneScriptFormsDesigner.Base64ToImage(str_addForm);
            _addForm.Name = "_addForm";
            _addForm.Size = new Size(221, 26);
            _addForm.Text = "Добавить Форму";
            // 
            // _useSnapLines
            // 
            string str_useSnapLines = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAd0lEQVR42u3YSw7AIAgFwHr/Q7er7uuvoM7bC0xiSLRcm6RED5ARckfOCgKyIKS3dlU9EBAQkO7GowMCsjokNCCVea9dS79PZ0FAQEBAQEBAQEDSQKbnCMjsp+vIWUFAMkD+XgzDvkxBQEAOhUQHBCQbZKlsA3kAIttEM9KSwFkAAAAASUVORK5CYII=";
            _useSnapLines.Image = OneScriptFormsDesigner.Base64ToImage(str_useSnapLines);
            _useSnapLines.Text = "Использовать линии привязки";
            _useSnapLines.Click += _useSnapLines_Click;
            // 
            // _useGrid
            // 
            string str_useGrid = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAf0lEQVR42u3UsRGAMBDEQNwX5dMXkDvgM3Q30oyzC7zJr6Ok9fcHhHxA7iEUt0uFXO87GyDbrhYSmxBaQmjVQnBndboTQtvVQmITQksIrVoI7qxOd0Jou1pIbEJoCaFVC8Gd1elOCG1XC4lNCC0htGohuLM63Qmh7WohsQmh9QCKkWUzuKisgQAAAABJRU5ErkJggg==";
            _useGrid.Image = OneScriptFormsDesigner.Base64ToImage(str_useGrid);
            _useGrid.Text = "Использовать сетку";
            _useGrid.Click += _useGrid_Click;
            // 
            // _useGridWithoutSnapping
            // 
            _useGridWithoutSnapping.Text = "Использовать сетку без привязки";
            _useGridWithoutSnapping.Click += _useGridWithoutSnapping_Click;
            // 
            // _useNoGuides
            // 
            _useNoGuides.Name = "_useNoGuides";
            _useNoGuides.Size = new Size(316, 26);
            _useNoGuides.Text = "Не использовать ориентиры";
            _useNoGuides.Click += _useNoGuides_Click;
            // 
            // _deleteForm
            // 
            string str_deleteForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABLklEQVR42u2asQrCMBCGDSoquLmIi4iuLu6uvrO7u4PdKg4uPoKi1XoHVlultTVc7xrvg2soheT/KCVpU1NzBMMdQEUyRELuMDYOeGhBnbjTWNJGkfidQKEjd6qcdFAgOkERH2rCncoSH0UuUA3uJJYEKBJA1bmTWHJVEWGoiDQ+RKq2ZInmQBWRgopIQ0Wk8b8i3G+P33KpSJHBKPnfZ0QqtCIh3TN1M8mVOp0I9LyEZkEkgngQclqGyBaaMaHICkLOXRBZQ8iZCyI78+qfVOQATZ9QZA8hh+Qij949aAYpl5tQ3ZRrZ6hNRtc9CDhKDlVMxJmZPe+8ULYM6aKxTBldokhDRaShItJIiMS33qoqErzv6lZJJpHbqe1pZ34YiOD+dvUrz184nEBFpHEHIdCPhqDjZfIAAAAASUVORK5CYII=";
            _deleteForm.Image = OneScriptFormsDesigner.Base64ToImage(str_deleteForm);
            _deleteForm.Name = "_deleteForm";
            _deleteForm.Size = new Size(221, 26);
            _deleteForm.Text = "Удалить Форму";
            _deleteForm.Click += _deleteForm_Click;
            // 
            // _stripSeparator1
            // 
            _stripSeparator1.Name = "_stripSeparator1";
            // 
            // _generateScript
            // 
            string str_generateScript = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABzklEQVR42u2aMU7DMBSGn+NCGRjgDjAxInEIxMgtWBiYGEoAqQsgWLgFEyBAcAMkkBhYgDtQRJFomzTYQBzjkiZO4jiO/A+VWjkv/xf7vby4QRApALOE2IeB5hkE4s0f3fZQjmClaX2pyTwzAFPMixAWwAKoAjg+f6lUZVpbmfvjTxqg++mPBJ2ewvUBEE+gWoUvoeedBURiVAcgQ0CwAKoAqLnfMZA0hspV1FvNcB6VzAAPcHdxEju26c9CD79KxV9cXpUDkElimsD89ySALKIAbtQ9Q+i7EABaQvll1iFX5+n6Bt69NwtQOgAVOVHwcHYFHu7qA+DFm+PFVyWZJC4dII1qB1CpJSQDECax9hnIW4WMBaByq5ADFkA3gKwqlwNGA8TdedPK1b2EagEw33qMTWKx/68lwP3pJQSNDz0AVOPKaNIeUOWfB0SADrdfX4T+W35KyygFODhsgz8cst+8YEACp+NCnK/NjW09ALt7LQLgZzp+EjcsQG6A9v4W9H3PAmgFAHAIRF/6eOw4gJEzFkJ5MxdB/Eh2NiYwjq1G4bai0OIU/x9Z0feDUOGeqHKAvE2gpGoGUAREGQD0gUl81YDRGCg0UpYM0rf3L8g8IyNDfeOmAAAAAElFTkSuQmCC";
            _generateScript.Image = OneScriptFormsDesigner.Base64ToImage(str_generateScript);
            _generateScript.Name = "_generateScript";
            _generateScript.Size = new Size(221, 26);
            _generateScript.Text = "Сформировать сценарий";
            _generateScript.Click += _generateScript_Click;
            // 
            // _stripSeparator2
            // 
            _stripSeparator2.Name = "_stripSeparator2";
            // 
            // _loadForm
            // 
            string str_loadForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAA/klEQVR42u3Z0QqDMAwFUDO2vez/f3UwN+iqOKE2qdUVe2NzoS/6kmMMVEvdSUK1CzBIAuI0P4ihkLtfr0IPoypkTyfgMBHEZRRFMf7h1xMGkoNIYI6otTwEADPMdl8EAoAJLv4FOaBix19WBNmCgYdMRRpEailixt0JC1GECDRtQdDmZzk3WRA0BIeJIBq6sQuCijAIWlaHnRTORzc1wIX38bthEMScAsLthsXfQagIBjLvRgxiEIPIn70ShFC/qhaQy69+FoLaDQYS/GNQsbdqE6IMYZDqkV4rgwAgGoGgYoRTsTRESdLHCorCQq5+vWtXtiE3vz6rMq0xCFq+0/uOMfSQbkgAAAAASUVORK5CYII=";
            _loadForm.Image = OneScriptFormsDesigner.Base64ToImage(str_loadForm);
            _loadForm.Name = "_loadForm";
            _loadForm.Text = "Открыть форму";
            _loadForm.Click += _loadForm_Click;
            // 
            // _saveForm
            // 
            string str_saveForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABBUlEQVR42u3awQ6CMBAEUPai/v/f6qXCgYTAbruF0p3qTMIBgW1fS4RYZfqRSHQHCKmAJNB+uS94zNs7ELHmOW+fK5DImTiN2UPSbkeUC5J17GpEH0QXxoRYHQ2ALHlNhdu9O0Tr7FprW1s5LzszsBDjXHNmwiDbUbcgNRh4iBczBMTAiLmDDClhhoIomHjIrlFCwiAeZFdIa4y3bjOIUqxpSoPTFHIXxjPDzSFRIQQthKBliG+tE+3iPkcq28Z7sntrD/Gu5alPCCGEEEIIIYQQohTuDSksNfwhpNTQ3RDruBtiFYuE1Pwaf8AAJ7s+sgTlDwO5HBZGc7cH6syI+8MRQwhavjI5HUJUEs5VAAAAAElFTkSuQmCC";
            _saveForm.Image = OneScriptFormsDesigner.Base64ToImage(str_saveForm);
            _saveForm.Name = "_saveForm";
            _saveForm.Text = "Сохранить форму";
            _saveForm.Click += _saveForm_Click;
            // 
            // _saveFormAs
            // 
            _saveFormAs.Image = OneScriptFormsDesigner.Base64ToImage(str_saveForm);
            _saveFormAs.Name = "_saveFormAs";
            _saveFormAs.Text = "Сохранить форму как";
            _saveFormAs.Click += _saveFormAs_Click;
            // 
            // _stripSeparator4
            // 
            _stripSeparator4.Name = "_stripSeparator4";
            // 
            // _exit
            // 
            _exit.Name = "_exit";
            _exit.Text = "Выход";
            _exit.Click += _exit_Click;
            // 
            // _edit
            // 
            _edit.DropDownItems.AddRange(new ToolStripItem[] {
            _unDo,
            _reDo,
            _stripSeparator3,
            _cut,
            _copy,
            _paste,
            _delete});
            _edit.Name = "_edit";
            _edit.Size = new Size(69, 24);
            _edit.Text = "Правка";
            // 
            // _unDo
            // 
            string str_unDo = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA2klEQVR42u3YSw6AIAwEULz/oTGamBAjyqdlZky7YSfzRH5uOWnXFoAABMDoQSnlfDaCgCP80UoCrvCSgDK8HOAeXgrwFF4GUAsvAXgLTw/4Ck8NaAk/WrPoT4BneAvIK2BV+BlIFYAIP4KgBPQg6D6hXgTNJHYDsCNgG1nrSzEDtHTavQQaIOCHudmRpThOz4wsxYUGAnjqWA5w71wSUAaQmcS1IBLLqFVBNjLvsL8DmB3mAjAAMLnQoAAmV0oUwOxSvxrg8ltlBcD9xxZ7BQBdAUBXANAlD9gBBDWIAQ4VHAYAAAAASUVORK5CYII=";
            _unDo.Image = OneScriptFormsDesigner.Base64ToImage(str_unDo);
            _unDo.Name = "_unDo";
            _unDo.ShortcutKeys = Keys.Control | Keys.Z;
            _unDo.Size = new Size(212, 26);
            _unDo.Text = "Отменить";
            _unDo.Click += _unDo_Click;
            // 
            // _reDo
            // 
            string str_reDo = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA10lEQVR42u3YSw7EIAwDUHr/QzOaXqD52IkjJRt21A+JEnjumV3PAhawgIaPnnPvOwwG/EcEohWAQLQDsggJQAYhA4gipAARhBzAi5AEeBBpgCVMtCyIMIAZ3INwA6qCWxEuQEf4L4QZ0Bk+DVANPwKQ3sTK4aEA1+lZdZAxwlvmhbUSyJWyzgtt5pArZZkX3k5XAigXmioA7UpZAaBe6tmbmP6swvyNlj1ssRCoorYSFShqM7cAFCCKkAJEEHIAL0IS4IFIAyyQEYDuWkB3LaC7FtBd4wE/1ESIAWn6qDIAAAAASUVORK5CYII=";
            _reDo.Image = OneScriptFormsDesigner.Base64ToImage(str_reDo);
            _reDo.Name = "_reDo";
            _reDo.ShortcutKeys = Keys.Control | Keys.Y;
            _reDo.Size = new Size(212, 26);
            _reDo.Text = "Вернуть";
            _reDo.Click += _reDo_Click;
            // 
            // _stripSeparator3
            // 
            _stripSeparator3.Name = "_stripSeparator3";
            // 
            // _cut
            // 
            string str_cut = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABNUlEQVR42u2Y0Q7CIAxF4f8/ekYjZpmU3sItlIS+OTI9BxDa5ivtHfkIHIEjsHc0BXJK1eHrMxQjqgISeESRPwEUPooEJHCHfI6HF6gBektokwgLiC/9hnkCyNatTmYEgZFDY+kWshwY8BbSACPBiwKWH7AKsMBVAQ8JNrwqwBLxADcJWECeAJ7wZgEEqkDMSkm60+nWbTkznxqqB2qXnveWoQoU4Br8+9mMeoJWkQmr8f3IB6cKSP+H8vy+QvTMlbGFWuPS9goh0APPlqAeoyg8U8QsYLmNZzQHYIHuislZQhVgACxJ5jxmzuU7Zze23AsarWRklJRMiS2KelNNDLZVaI2t0dVYLmARcekLUfMaoNIbFgjf3NVmIhK8KIBIRIBvCrREosBDAtHjCKyOI7A6thd4ARhzzAEzNxSrAAAAAElFTkSuQmCC";
            _cut.Image = OneScriptFormsDesigner.Base64ToImage(str_cut);
            _cut.ImageTransparentColor = Color.Magenta;
            _cut.Name = "_cut";
            _cut.ShortcutKeys = Keys.Control | Keys.X;
            _cut.Size = new Size(212, 26);
            _cut.Text = "Вырезать";
            _cut.Click += OnMenuClick;
            // 
            // _copy
            // 
            string str_copy = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABOElEQVR42u3WrQ7CMBAH8DUIBAaD4AnAYtCEZ8DxEih4BhwejeMZCBoECRiQ6CUEMwghY7AlJCPpPq7tbddwfzOxNe2vubtMOJZHlH0ABqS8CwrYwwoAKiIT0F94oM1Xw1q0buf6otOoxC8BBYEGOHsvcbkFDjYCFRA+sRHoAGxEIQBMRGEALIQRwPfQBgIGUQOAEUYBs80DtPmoW43WHa++aNfVSosEIOyX+zNwVBBkAOFTBQECZNW6LkAFQQ4ARZAqIRUEWYAEoQ84rLfSD915TwuQMwww3sRAJC3A52Cy9bgASAkByyPxIhiAOUbzhAGqTTxdnnIfcjJo0QPIpkxW0KdQUmQlBGnitNJjgAbAaY73eQ0/ifcEAyAhOUZtBkBC8lfivwGmwgAG2ARADgNIhgFlx3rAG9GomUA3I+5MAAAAAElFTkSuQmCC";
            _copy.Image = OneScriptFormsDesigner.Base64ToImage(str_copy);
            _copy.ImageTransparentColor = Color.Magenta;
            _copy.Name = "_copy";
            _copy.ShortcutKeys = Keys.Control | Keys.C;
            _copy.Size = new Size(212, 26);
            _copy.Text = "Копировать";
            _copy.Click += OnMenuClick;
            // 
            // _paste
            // 
            string str_paste = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABIklEQVR42u3awQnCMBQG4OTkFL07gY6Rg6eCriAFcQoRiiso9CbFKcQNvIpLeHoWaaGWtEmapH2h77/12b68DxPxUM4CDx97ADSAY5ZHu1i86rX0fJsnG/FED5ANX+V0uUbb9eqNGsAYQK0lb6+hBzQHbasPC4DOy/92LQCtpXshVQ+BVqmBOGT5ch+Le4+ljRFdD4BR2Tjc+AMrAAD87i02iRNBsbnKfrzZzz2gGt4HQILwDeCs+3BqLcuh1mJQgI8QgAAEGBkg+S334vQKSB8fb99SspgBAQhAAAJMCFAuaBVJPwJoA1yHAAQIDWB6iFV4AkxuCxGAAJaA4P9KBA9wHWuAChEEQBWMACMEVoA2AjNAJyjPwLQAvoZXzRr82ypful5dQCOEOe0AAAAASUVORK5CYII=";
            _paste.Image = OneScriptFormsDesigner.Base64ToImage(str_paste);
            _paste.ImageTransparentColor = Color.Magenta;
            _paste.Name = "_paste";
            _paste.ShortcutKeys = Keys.Control | Keys.V;
            _paste.Size = new Size(212, 26);
            _paste.Text = "Вставить";
            _paste.Click += OnMenuClick;
            // 
            // _delete
            // 
            string str_delete = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAABmJLR0QA/wD/AP+gvaeTAAACJUlEQVRoge2YvW/TQBiHn7sjY9UFiSIYOjSiVGyt5HYAUkLyR0NVRwgJkBiRGGAEqaIsSQZKFfsYiFFUXXJ3vtdIiHtG34d/j+9svzZkMplMJhPIfFgcTEdFv6v5p4OTvfmwOIgZo0M7zsaH+wp7Zmr7av78+GF8vM1MR0XfmGqisOV8fPQodJwK6TQbH+7rypwDd5eHLqzSz7bO3n5sE/Ym01HRN7UtgXvLQ9+sqYdbL95/8I31CjjCN4hIOMI3BEl4t1DvuncF/HQ07Shbn6dspw3hAWrqW5VvjqAt9GNwvFuZugR2Hc2tVsITPnjOIAGQlZAKDxECICMhGR4iBSBNQjo8tBCAdhJdhIeWAhAn0VV4SBCAMIla2UVX4SFRADwSikssNXDHMVTkRZgsAN6VcCFWiogIQJSEaB0lJgC/JRamfq3c+x0L31H6iVR4iCinQ7ju2Z62mHXtoldriZhA86i0ip0N3W6nFoA3Ebkonue8C7H7IHkFNoa3XAIXjmHJpXhDkoD3Dav106oyj4EvjnYRidZbKKY8mA5O9oypSuC+r28srQTa1DZdSUQLpBRmXUhECUhUlUuJScocqwTfxFIl8fbkzedKq1Pgq6M5+sYOWoEu6vm/9lHf5ceIxNzeLaQWlV7TL/ltuv3y3acN20mjF2vrqj/5Qk40Oz16oLUu+Rd/LTasSCjJ8A0rEr3Q8NF0/nt9VPRjf69nMplM5v/mF/i6x8b172ZWAAAAAElFTkSuQmCC";
            _delete.Image = OneScriptFormsDesigner.Base64ToImage(str_delete);
            _delete.Name = "_delete";
            _delete.ShortcutKeys = Keys.Delete;
            _delete.Size = new Size(212, 26);
            _delete.Text = "Удалить";
            _delete.Click += OnMenuClick;
            // 
            // _view
            // 
            _view.DropDownItems.AddRange(new ToolStripItem[] {
            _form,
            _code});
            _view.Name = "_view";
            _view.Size = new Size(50, 24);
            _view.Text = "Вид";
            // 
            // _form
            // 
            string str_form = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAA1klEQVR42u3a4QqCMBQF4EaB9R69/wv5IAktavfHYippebVzdz0HJvhjcj5EpsxwcJKALkDIBOSJLqMxyKFJo0O3UeYskPJOCOiGbvVlLgLIJwJp07iiWynTCuSexgndRJkokJjGEd1EmQchxkKItYwgtb2y5DWQECshxFr2C0F/q8z1IoQQQlaCWA0hP119iwwKbgf5x8NUlCSEEEJQkDlMWDjvw/z9ruyEEELI9ASrIcRafELKrbdaIXG4q1sTptfb1fa0mx8GctCL3tK8f+FwEUKs5QXoOIWG//RH0wAAAABJRU5ErkJggg==";
            _form.Image = OneScriptFormsDesigner.Base64ToImage(str_form);
            _form.Name = "_form";
            _form.Size = new Size(50, 24);
            _form.Text = "Форма";
            _form.Click += _form_Click;
            _form.Enabled = false;
            _form.CheckState = System.Windows.Forms.CheckState.Checked;
            // 
            // _code
            // 
            string str_code = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAvElEQVR42u3WQQqDMBSE4eQ2FWk3Hsoz9VBd6cLb6BNcdSE1IWby+g8ICYjOpz4wru8heEj8C0gcP+sNHSbr8PIA2bNYj644xM6JJdp/PagsjApkseMRMj4zCch+fVvPtuxD4puRgRz7ZIwUJAcjB0nFSEJSMLKQqxgJyI+ZrcfTA+S0R1XIVTAQIC1BSv3in90LCJCWIHcGCJAWIW6GHQgQhh0IkCoQN8MOBAjDDgQIECBAKkNUAqSluIFsV0sN9+kjczYAAAAASUVORK5CYII=";
            _code.Image = OneScriptFormsDesigner.Base64ToImage(str_code);
            _code.Name = "_code";
            _code.Size = new Size(50, 24);
            _code.Text = "Сценарий";
            _code.Click += _code_Click;
            _code.CheckState = System.Windows.Forms.CheckState.Unchecked;
            // 
            // _tools
            // 
            _tools.DropDownItems.AddRange(new ToolStripItem[] {
            _tabOrder,
            _stripSeparator5,
            _run,
            _stripSeparator6,
            _settings});
            _tools.Name = "_tools";
            _tools.Size = new Size(113, 24);
            _tools.Text = "Инструменты";
            // 
            // _tabOrder
            // 
            string str_tabOrder = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA3ElEQVR42u3ZSw6AIAwEULj/oTExcdOApjD9menWiPOiQNU+Wu3qaEBvbXvIcZ8eCDgJv4v4BMhQqwsgwsMBq1DyIqjwIQBkeAJ2AGgEJ/FbuK/BUy6jGsApwmwj0wC8awqYpBzrQ9jS3j4CCLAATEC1JjEBlQHo5u6pZQuTrZXQItI1c1BARDvtDvAKT8DbQGkfoVm4UpNYhiu5jGoAlgiXjSyiCIiu/wJE0npvZAQQcAgQGPONbHeBSNVKmHwb9Wzm4ADvdjoEEPl3hgA0gpP4tFIuo14I040sc11VMcAB84B/6gAAAABJRU5ErkJggg==";
            _tabOrder.Image = OneScriptFormsDesigner.Base64ToImage(str_tabOrder);
            _tabOrder.Name = "_tabOrder";
            _tabOrder.Size = new Size(217, 26);
            _tabOrder.Text = "Порядок обхода";
            _tabOrder.Click += _tabOrder_Click;
            // 
            // _stripSeparator5
            // 
            _stripSeparator5.Name = "_stripSeparator5";
            // 
            // _run
            // 
            string str_run = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABkUlEQVR42t3Z0Q7CIAwFUPv/H42JmWYD2t4WCkUejDrAHjsGbPT6k0K7AwBL0RyZIVrw6SEWQEoIDyjVoTbsFBAcwIe+FeIDJIO0kSLBJ4OMIxJAnhF7AB3E7+UoxGaIjPgGZh/kSyE8gjuzDZfd5s1mBDX1exgmG9EQ7HRqYygghP+QAMFjhGxEQfR5Yhwiq5YgdIw5G7MhthnbMthJP4tmQXzLDuTyC2QDhdinYO8CENh3sF1sR2g4MFbp4DEIqYK+wKt/bCOCq3QcoldxD4LGr6g2SFKEDpFKIoTUSI4yGQJtOG+LGoSwNJY3PJsR6yFBiLWQQATa0ey7gtMRSGflLnlURjHkXrDGQcwYci9Y4yEwprcKAHZ6URAfhlvKrIaUK26q9tIFadx0Ztt7T4OUKt4hjJypOEhh1lgSJh2kl4n7dzVG+EOQWzoxEA2hYPhdZn+QT0d8OkQywWRGKtIEE3K/WQ1aGPweSNjdf5IGuBPBYUKfxbBjYRBRQ8KfjLEZGUQsLyrkBMQVJzQRpi9vUR3TM0g50FMAAAAASUVORK5CYII=";
            _run.Image = OneScriptFormsDesigner.Base64ToImage(str_run);
            _run.Name = "_run";
            _run.Text = "Запуск";
            _run.Click += _run_Click;
            _run.ShortcutKeys = Keys.Control | Keys.F6;
            _run.ShowShortcutKeys = true;
            // 
            // _stripSeparator6
            // 
            _stripSeparator6.Name = "_stripSeparator6";
            // 
            // _settings
            // 
            string str_settings = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABI0lEQVR42u3YwQ6EMAgEUP3/j3aTPRhFaGkLOLNZrqKZp9JY92P7jdr/kKgA29aNcHzbQCEeADTEAlzDXns8iHKIhpBBZxClEInQQs4iXoNEI8og2YhySCRCXjMd0gq6iqCGWItGGSTitWot3zQQ2fvajFhBPRh1JpCGXR7Xeqzz4SAaRisLCAXpYTxPCQZigbyLRCrEEyrqxqRBKhFpkGxECeQNRDhkBNHbLXq2xCmQVUSvup8wEZBMRNnPhxVExPyEQFAQSxAkxDQEDTEFQUQMQ1ARQxBkhBuCjnBBGBBdCAuiCWFCmBA2hAphRDwgrIgbhBlxQtgRKoQRMQRBRpwQGbT1ixIRYUKuYRkQN4gWWhYq4gFpYZARKkQDoSOaELb6AFfXABC6bvmCAAAAAElFTkSuQmCC";
            _settings.Image = OneScriptFormsDesigner.Base64ToImage(str_settings);
            _settings.Name = "_settings";
            _settings.Text = "Параметры";
            _settings.Click += _settings_Click;
            // 
            // _help
            // 
            _help.DropDownItems.AddRange(new ToolStripItem[] { _about });
            _help.Name = "_help";
            _help.Size = new Size(77, 24);
            _help.Text = "Помощь";
            // 
            // _about
            // 
            string str_about = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABCklEQVR42u3WwRKDIAyE4fLmvjntpRenSDbZTaBDzo7+n4NCu157TzuAA1gE8LlPB69vSwDQcDbGDWCEMyAwQBEeQUAAdbwHYQZkxaMIEyA7HkFMAVXxVsQjwBvfBw9tzvs9IaiAbl23xE1vCFDFsxEUABrvQUCAjHgUMEJIAd9AyzU0ABL/FHcPY/2Z7ogwYBS3FcCL3AbA3NjSAawPuATAjk8FKOKXAETi0wCq+FIAI/4AVpgpYGXEpTiN/hrVEjIDIois////A7yIjOODFDBCKN7+FMBCqOJNgAgiOrN4M6ACYYmHAJkIazwMyEAg8S6ACoKGhwEsiDecBvBCouF0QNUcQPVsD3gDeqycMcHL1j4AAAAASUVORK5CYII=";
            _about.Image = OneScriptFormsDesigner.Base64ToImage(str_about);
            _about.Name = "_about";
            _about.Size = new Size(187, 26);
            _about.Text = "О программе...";
            _about.Click += _about_Click;
            // 
            // pnl4Toolbox
            // 
            pnl4Toolbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pnl4Toolbox.Controls.Add(listBox1);
            pnl4Toolbox.Dock = System.Windows.Forms.DockStyle.Left;
            pnl4Toolbox.Location = new Point(0, 26);
            pnl4Toolbox.Name = "pnl4Toolbox";
            pnl4Toolbox.Size = new Size(163, 489);
            pnl4Toolbox.TabIndex = 2;
            // 
            // listBox1
            // 
            listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 16;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(159, 485);
            listBox1.TabIndex = 0;
            // 
            // pnl4pDesigner
            // 
            pnl4pDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            pnl4pDesigner.Location = new Point(163, 26);
            pnl4pDesigner.Name = "pnl4pDesigner";
            pnl4pDesigner.Size = new Size(726, 489);
            pnl4pDesigner.TabIndex = 3;
            // 
            // pnl4splitter
            // 
            pnl4splitter.BackColor = Color.LightSteelBlue;
            pnl4splitter.Location = new Point(163, 26);
            pnl4splitter.Name = "pnl4splitter";
            pnl4splitter.Size = new Size(5, 489);
            pnl4splitter.TabIndex = 4;
            pnl4splitter.TabStop = false;
            // 
            // pDesignerMainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(889, 515);
            Controls.Add(pnl4splitter);
            Controls.Add(pnl4pDesigner);
            Controls.Add(pnl4Toolbox);
            Controls.Add(menuStrip1);
            string str_Icon = "AAABAAEAAAAQAAEABABooAAAFgAAACgйQAAAAIAAAEABйAAKййAAEййAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8ккккккккккккккккккккккккккккккккккккккккккйййййAAAеееец////8ййAAAееееццййAAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPее//8Aццц//ййDццц/8Pцццц//8ййAее//8AAP8йййййPAAAADцц/8ADцццц//wййDгкййAAAP8AAAD/ййAAAADwйй8AAAAADц/////8AAAцццц//ййAPццц/wкййAAAP8AAAAA/wййAAAPййDwйц////8AAAAPцццц8ййAццц/wкййAAAP8йP8ййAAA8ййP/wAAAADц///8AAAAADццццwййDццц/кййAAAP8йAD/ййD//wййAP//AAAAц//8йAццццййAPцццкййAAAP8йAAA/wйAAD///ййй//AADц/8йAAPг/8ййAццц8кййAAP8йAAAAP8йA//йййAAAA//8Pц8йAAADг/wййDцццwкййAP8йAAAAAD/AAAAD//wййййDц/8йAAAAAг/ййAPцццкййAP8ййA/wAA//ййййAAAAD/////8ййPццц//8ййAццц8кййP8ййAAP///wййййй////8ййADццц//wййDцццwйDе/////8ййAAAD//wйййййAA//8ййAAAццц//ййAPцццйDе/////8ййAAAAA8йййййAAAAA8ййAAAAPццц8ййAццц8йPе/////wкйййййAAAAццц/wййDцццwйец8кйййййAAAццц//ййAPцццйDец/кйййййAAццц//8ййAццц8йPец/wкйййййгwййDцццwйец//8кййййAAAAAг/ййAPцццйDец///кййййAAAAг/8ййAццц8йPец///wкййййAAг//wййDцццwйец////8кййййAццццййAPцццйDец/////кййййцццц8ййAццц8йPец/////wкйййAAAAцццц/wййDцццwйецц8кйййAAAцццц//ййAPцццйDецц/кйййAAцццц//8ййAццц8йPецц/wкйййцгwййDцццwйецц//кйййDцгййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц8кйййADцццц//wййDцццwйецц/wкйййAAцццц//ййAPцццйDеццwкйййAADцццц/8ййAццц8йPеццкйййAAAAцццц/wййDцццwйеццкйййAAAAAPццццййAPцццйDец/////8ййййADц/ййййAAцццц8ййAццц8йPец////8ййййAц////8йййAAAAADццццwййDцццwйец/////wйййAAAADцц/8йййAAAAAццццййAPцццйDец////wйййAAAADцц///йййAAAADг//8ййAццц8йPец////йййAAAAцццйййAAAAг//wййDцццwйец////8йййAAAццц/wйййAADг//ййAPцццйDец////wйййAAццц//8йййAAPг/8ййAццц8йPец///wйййAADццц//wйййAADг/wййDцццwйец////йййAADг8йййAAPг/ййAPцццйDец///8йййADг//йййAAг/8ййAццц8йPец//8йййAAPг/8йййADг/wййDцццwйццгwйййййAPццццйййййAPц////ййAPцццйDццгйййййAAцццц/wйййййц////8ййAццц8йPццццц//8йййййAцццц//йййййDц////wййDцццwйццгwйййййDцццц//wййййAAAAAPц////ййAPцццйDццгйййййAPцццц//йййййц////8ййAццц8йPццццц//8йййййAцццц//8ййййAAAAADц////wййDцццwйццгwйййййцгwййййAAAAAPц////ййAPцццйDццгйййййDцг8ййййAAAAAц////8ййAццц8йPццццц//8йййййPцгwййййAAAADц////wййDцццwйццгwйййййцг/ййййAAAAAPц////ййAPцццйDццгйййййDцг8ййййAAAAAц////8ййAццц8йPццццц//8йййййPцццц//8ййййAAAAADц////wййDцццwйццгwйййййDцццц//wййййAAAAAPц////ййAPцццйDццгйййййAPцццц//йййййц////8ййAццц8йPццццц//8йййййAцццц//8ййййAAAAADц////wййDцццwйццгwйййййDцццц/8йййййPц////ййAPцццйDццгйййййAAцццц/wйййййц////8ййAццц8йPццццц//8йййййADццццwйййййDц////wййDцццwйец//йййAAAAг//wйййAPг/ййAPцццйDец///8йййADг//йййAAг/8ййAццц8йPец///wйййAAг/йййAADг/wййDцццwйец////йййAADгйййAAAPг/ййAPцццйDец///8йййAAAццц//8йййAAPг/8ййAццц8йPец////йййAAAPццц8йййAAAг//wййDцццwйец////8йййAAADцц/////8йййAAADг//ййAPцццйDец/////йййAAAAцц////8йййAAADг//8ййAццц8йPец////8йййAAAAAцц//йййAAAAAPг//wййDцццwйец/////wййййц/////wййййццццййAPцццйDец/////8ййййAц//8ййййAцццц8ййAццц8йPец/////wййййAAAA8PййййAAAADццццwййDцццwйецц8кйййAAADцццц/ййAPцццйDеццwкйййAAAPцццц8ййAццц8йPецц8кйййAAPцццц/wййDцццwйецц/wкйййAPцццц//ййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц//кйййцгwййDцццwйецц//8кййAAAAADцгййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц8кйййAAPцццц/wййDцццwйецц8кйййAAADцццц/ййAPцццйDец/////8кйййAAAAAцццц8ййAццц8йPец////8кййййPг//wййDцццwйец////8кййййADг//ййAPцццйDец///8кййййAAAг/8ййAццц8йPец//8кййййAAAAPгwййDцццwйец//8кййййAAAAADгййAPцццйDец/8кйййййAццц//8ййAццц8йPец8кйййййAAPццц/wййDцццwйец8кйййййAAADццц/ййAPцццйDецwййAAAA8йййййAAAAA8ййAAAAPццц8ййAццц8йPец8ййAAA//йййййAAAA//ййAAAPццц/wййDцццwйец//ййAA////йййййAP///wййAPццц//ййAPцццйDец//wййцййййAAAAц8ййPццц//8ййAццц8йPец//8йAAAAAц//ййййAPц//йAAAAAPгwййDцццwкййAAAPйAAAA8йAD/8йййAAAD/8Aц/wйAAAPг/ййAPцццкййAAAADwйAA8йAAAD//wййAAAAD///AADц/8йAAPг/8ййAццц8кййAAAAA8йA8ййP//8ййP//AAAAAPц//йAPг//wййDцццwкййAAAAAPй8ййAAADwйй/wйц///wAAAAAPццццййAPцццкйййDwAAAA8ййAAAAPййD8йDц///8AAAAPцццц8ййAццц8кйййA8AAA8ййAAAAA8ййPwйPц////AAAPцццц/wййDцццwкйййAPAA8ййAAAAADwйй/йAц/////wAPцццц//ййAPцццкйййAADw8йййPййD8йDц/////8Pцццц//8ййAццц8кйййAAA8йййA8ййPwйPе///wййDцццwйнуууууzMzMzwйй/йAе////ййAPцццйDMнуууууzMzPййD8йDе///8ййAццц8йMнуууууzMzM8ййPwйPе///wййDцццwйнуууууzMzMzwйй/йAе////ййAPцццйDMzMzMzйййййAууwAAAуzMAAAMzMzMzPййD8йDе///8ййAццц8йMzMzMwAццццц8AуzMzMAP//8AzMzMzAD///AMzMzM8ййPwйPе///wййDцццwйzMzMwPццццц//8MуzMD/////8MzMzAцDMzMzwйй/йAе////ййAPцццйDMzMwPццг/DMуDц/DMzAц/wzMwPййD8йDе///8ййAццц8йMzMzAццг/8MуwPц8MzMDц/DMzA8ййPwйPе///wййDцццwйzMzAеDMzMzMwPц//DMDц//wzA/wйй/йAе////ййAPцццйDMzMDццг//8MzMzMzAц//8MwPц//DMD/ййD8йDе///8ййAццц8йMzMwPццг//wуDц//wzAц//8MwP8ййPwйPе///wййDцццwйzMzAеDMzMzMwPц//DMDц//wzAцц///йAе////ййAPцццйDMzMDццг//8MzMzMzAц//8MwPц//DMDц//wуйDе///8ййAццц8йMzMzAццг/8MуwPц8MzMDц/DMzAц/wуwйPе///wййDцццwйzMzMDццг/wуzAц/wzMwPц8MzMDц/DMzMzMzйAе////ййAPцццйDMzMzAццгwуzMwP/////wzMzMD/////8MzMzAцDMуйDе///8ййAццц8йMzMzMwAццццц8AуzMzMAP//8AzMzMzAD///AMzMzMwA///wDMуwйPе///wййDцццwйуwйййййMууAAAMуzAAADMуwAAAуzMzйAе////ййAPцццйDMннууйDе///8ййAццц8йMннууwйPе///wййDцццwйннууwйAе////ййAPцццйAMннуzMzMwйADе///8ййAццц8ккййййAAPе///wййDцццwккййййAAе////ййAPцццккййййAADе///8ййAццц8ккййййAAPе///wййDцццwккййййAAе////ййAPццц8ккййййAе////8ййAццц/wккййййDе////wййDццц/8ккйййAAAAADе/////ййAPццц//8ккйййAAAец8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8кккккккккккккккккккккккккккккккккккккккккккккккййййAAADMннннууййAMннннууwййAннннууzййADMннннууййAMннннууwййAннннууzййADMннннууййAMннннууwййAннннууzййADMуzMккAAуууzMAAAAAMууzMAAAAAMууzMAAAAAMуzMййAMуzADее/8AууzMzMzAD/////AMуzMzMzAD/////AMуzMzMzAD/////AMуwййAуwAее////8AууzMwAц//AMуzMwAц//AMуzMwAц//AMzMzMzййADMzMzMwPеец8MууwPц////DMуwPц////DMуwPц////DMzMzMййAMzMzMwPеец//DMуzMzMwPц/////wуwPц/////wуwPц/////wzMzMwййAzMzMwPеец///wуzMzMwPцц8MzMzMwPцц8MzMzMwPцц8MzMzййADMzMzAеец////DMуzMzAцц/wzMzMzAцц/wzMzMzAцц/wzMzMййAMzMzAеец/////wуzMzAцц//8MzMzAцц//8MzMzAцц//8MzMwййAzMzMDеец/////DMуzMDцц//wzMzMDцц//wzMzMDцц//wzMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzAеецц8MуzAцц////DMzAцц////DMzAцц////DMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzAеецц8MуzAцц////DMzAцц////DMzAцц////DMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzMDеец/////DMуzMDцц//wzMzMDцц//wzMzMDцц//wzMzййADMzMwPеец////8MуzMwPцц//DMzMwPцц//DMzMwPцц//DMzMййAMzMzMDеец///8MуzMzMDцц/DMzMzMDцц/DMzMzMDцц/DMzMwййAzMzMwPеец///wуzMzMwPцц8MzMzMwPцц8MzMzMwPцц8MzMzййADMzMzMDеец//wууDц/////8MуDц/////8MуDц/////8MzMzMййAMzMzMzAеец/wууzAц////8MуzAц////8MуzAц////8MzMzMwййAуwAее////8AууzMwAц//AMуzMwAц//AMуzMwAц//AMzMzMzййADMуwAее//AMууzMzMwA/////wDMуzMzMwA/////wDMуzMzMwA/////wDMуййAMуzMwккADMуууwAAAAAууzMwAAAAAууzMwAAAAAуzMwййAннннууzййADMннннууййAMннннууwййAннннууzййADMннннууййAAннннууwййAAMннннуzMzMwкккккккккккккккккккккккккккккккккккккккккккккккйййAAAD/gкAAB//4кAAAB/+кAAAAB/wкAAAAD+кAAAAAHwкAAAAAPкй4кAAAAABgкAAAAAEккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккAAIкAAAAABgкAAAAAHкй8кAAAAAD4кAAAAAfwкAAAAD/gкAAAAf/gкAAAH//gкAAB/w==";
            str_Icon = str_Icon.Replace("г", "ццц///");
            str_Icon = str_Icon.Replace("н", "уууууу");
            str_Icon = str_Icon.Replace("е", "цццццц");
            str_Icon = str_Icon.Replace("к", "йййййй");
            str_Icon = str_Icon.Replace("у", "zMzMzM");
            str_Icon = str_Icon.Replace("ц", "//////");
            str_Icon = str_Icon.Replace("й", "AAAAAA");
            Icon = new Icon(new MemoryStream(Convert.FromBase64String(str_Icon)));
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            Name = "pDesignerMainForm";
            Text = "Дизайнер форм для OneScriptForms";
            Load += pDesignerMainForm_Load;
            //* 18.12.2021 perfolenta
            FormClosing += pDesignerMainForm_Closing;
            //***
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            pnl4Toolbox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

            // Элемент управления: (pDesigner)pDesignerCore.
            IpDesignerCore = pDesignerCore as IpDesigner;
            pDesignerCore.Parent = pnl4pDesigner;

            // Добавим указатель.
            ToolboxItem toolPointer = new ToolboxItem();
            toolPointer.DisplayName = "<Указатель>";
            toolPointer.Bitmap = new Bitmap(16, 16);
            listBox1.Items.Add(toolPointer);

            // Добавим элементы управления.
            ToolboxItem toolButton = new ToolboxItem(typeof(Button));
            toolButton.DisplayName = "Кнопка (Button)";
            listBox1.Items.Add(toolButton);

            ToolboxItem toolCheckBox = new ToolboxItem(typeof(CheckBox));
            toolCheckBox.DisplayName = "Флажок (CheckBox)";
            listBox1.Items.Add(toolCheckBox);

            ToolboxItem toolColorDialog = new ToolboxItem(typeof(ColorDialog));
            toolColorDialog.DisplayName = "ДиалогВыбораЦвета (ColorDialog)";
            listBox1.Items.Add(toolColorDialog);

            ToolboxItem toolComboBox = new ToolboxItem(typeof(ComboBox));
            toolComboBox.DisplayName = "ПолеВыбора (ComboBox)";
            listBox1.Items.Add(toolComboBox);

            ToolboxItem toolDataGrid = new ToolboxItem(typeof(DataGrid));
            toolDataGrid.DisplayName = "СеткаДанных (DataGrid)";
            listBox1.Items.Add(toolDataGrid);

            ToolboxItem toolDateTimePicker = new ToolboxItem(typeof(DateTimePicker));
            toolDateTimePicker.DisplayName = "ПолеКалендаря (DateTimePicker)";
            listBox1.Items.Add(toolDateTimePicker);

            ToolboxItem toolFileSystemWatcher = new ToolboxItem(typeof(FileSystemWatcher));
            toolFileSystemWatcher.DisplayName = "НаблюдательФайловойСистемы (FileSystemWatcher)";
            listBox1.Items.Add(toolFileSystemWatcher);

            ToolboxItem toolFontDialog = new ToolboxItem(typeof(FontDialog));
            toolFontDialog.DisplayName = "ДиалогВыбораШрифта (FontDialog)";
            listBox1.Items.Add(toolFontDialog);

            ToolboxItem toolFolderBrowserDialog = new ToolboxItem(typeof(FolderBrowserDialog));
            toolFolderBrowserDialog.DisplayName = "ДиалогВыбораКаталога (FolderBrowserDialog)";
            listBox1.Items.Add(toolFolderBrowserDialog);

            ToolboxItem toolGroupBox = new ToolboxItem(typeof(GroupBox));
            toolGroupBox.DisplayName = "РамкаГруппы (GroupBox)";
            listBox1.Items.Add(toolGroupBox);

            ToolboxItem toolHProgressBar = new ToolboxItem(typeof(HProgressBar));
            toolHProgressBar.DisplayName = "ИндикаторГоризонтальный (HProgressBar)";
            listBox1.Items.Add(toolHProgressBar);

            ToolboxItem toolVProgressBar = new ToolboxItem(typeof(VProgressBar));
            toolVProgressBar.DisplayName = "ИндикаторВертикальный (VProgressBar)";
            listBox1.Items.Add(toolVProgressBar);

            ToolboxItem toolHScrollBar = new ToolboxItem(typeof(HScrollBar));
            toolHScrollBar.DisplayName = "ГоризонтальнаяПрокрутка (HScrollBar)";
            listBox1.Items.Add(toolHScrollBar);

            ToolboxItem toolImageList = new ToolboxItem(typeof(System.Windows.Forms.ImageList));
            toolImageList.DisplayName = "СписокИзображений (ImageList)";
            listBox1.Items.Add(toolImageList);

            ToolboxItem toolLabel = new ToolboxItem(typeof(Label));
            toolLabel.DisplayName = "Надпись (Label)";
            listBox1.Items.Add(toolLabel);

            ToolboxItem toolLinkLabel = new ToolboxItem(typeof(LinkLabel));
            toolLinkLabel.DisplayName = "НадписьСсылка (LinkLabel)";
            listBox1.Items.Add(toolLinkLabel);

            ToolboxItem toolListBox = new ToolboxItem(typeof(ListBox));
            toolListBox.DisplayName = "ПолеСписка (ListBox)";
            listBox1.Items.Add(toolListBox);

            ToolboxItem toolListView = new ToolboxItem(typeof(ListView));
            toolListView.DisplayName = "СписокЭлементов (ListView)";
            listBox1.Items.Add(toolListView);

            ToolboxItem toolMainMenu = new ToolboxItem(typeof(System.Windows.Forms.MainMenu));
            toolMainMenu.DisplayName = "ГлавноеМеню (MainMenu)";
            listBox1.Items.Add(toolMainMenu);

            ToolboxItem toolMonthCalendar = new ToolboxItem(typeof(MonthCalendar));
            toolMonthCalendar.DisplayName = "Календарь (MonthCalendar)";
            listBox1.Items.Add(toolMonthCalendar);

            ToolboxItem toolNotifyIcon = new ToolboxItem(typeof(NotifyIcon));
            toolNotifyIcon.DisplayName = "ЗначокУведомления (NotifyIcon)";
            listBox1.Items.Add(toolNotifyIcon);

            ToolboxItem toolNumericUpDown = new ToolboxItem(typeof(NumericUpDown));
            toolNumericUpDown.DisplayName = "РегуляторВверхВниз (NumericUpDown)";
            listBox1.Items.Add(toolNumericUpDown);

            ToolboxItem toolOpenFileDialog = new ToolboxItem(typeof(OpenFileDialog));
            toolOpenFileDialog.DisplayName = "ДиалогОткрытияФайла (OpenFileDialog)";
            listBox1.Items.Add(toolOpenFileDialog);

            ToolboxItem toolPanel = new ToolboxItem(typeof(Panel));
            toolPanel.DisplayName = "Панель (Panel)";
            listBox1.Items.Add(toolPanel);

            ToolboxItem toolPictureBox = new ToolboxItem(typeof(PictureBox));
            toolPictureBox.DisplayName = "ПолеКартинки (PictureBox)";
            listBox1.Items.Add(toolPictureBox);

            ToolboxItem toolPropertyGrid = new ToolboxItem(typeof(PropertyGrid));
            toolPropertyGrid.DisplayName = "СеткаСвойств (PropertyGrid)";
            listBox1.Items.Add(toolPropertyGrid);

            ToolboxItem toolRadioButton = new ToolboxItem(typeof(RadioButton));
            toolRadioButton.DisplayName = "Переключатель (RadioButton)";
            listBox1.Items.Add(toolRadioButton);

            ToolboxItem toolRichTextBox = new ToolboxItem(typeof(RichTextBox));
            toolRichTextBox.DisplayName = "ФорматированноеПолеВвода (RichTextBox)";
            listBox1.Items.Add(toolRichTextBox);

            ToolboxItem toolSaveFileDialog = new ToolboxItem(typeof(SaveFileDialog));
            toolSaveFileDialog.DisplayName = "ДиалогСохраненияФайла (SaveFileDialog)";
            listBox1.Items.Add(toolSaveFileDialog);

            ToolboxItem toolSplitter = new ToolboxItem(typeof(Splitter));
            toolSplitter.DisplayName = "Разделитель (Splitter)";
            listBox1.Items.Add(toolSplitter);

            ToolboxItem toolStatusBar = new ToolboxItem(typeof(StatusBar));
            toolStatusBar.DisplayName = "СтрокаСостояния (StatusBar)";
            listBox1.Items.Add(toolStatusBar);

            ToolboxItem toolTabControl = new ToolboxItem(typeof(TabControl));
            toolTabControl.DisplayName = "ПанельВкладок (TabControl)";
            listBox1.Items.Add(toolTabControl);

            ToolboxItem toolTextBox = new ToolboxItem(typeof(TextBox));
            toolTextBox.DisplayName = "ПолеВвода (TextBox)";
            listBox1.Items.Add(toolTextBox);

            ToolboxItem toolTimer = new ToolboxItem(typeof(Timer));
            toolTimer.DisplayName = "Таймер (Timer)";
            listBox1.Items.Add(toolTimer);

            ToolboxItem toolToolBar = new ToolboxItem(typeof(ToolBar));
            toolToolBar.DisplayName = "ПанельИнструментов (ToolBar)";
            listBox1.Items.Add(toolToolBar);

            ToolboxItem toolToolTip = new ToolboxItem(typeof(ToolTip));
            toolToolTip.DisplayName = "Подсказка (ToolTip)";
            listBox1.Items.Add(toolToolTip);

            ToolboxItem toolTreeView = new ToolboxItem(typeof(TreeView));
            toolTreeView.DisplayName = "Дерево (TreeView)";
            listBox1.Items.Add(toolTreeView);

            ToolboxItem toolUserControl = new ToolboxItem(typeof(UserControl));
            toolUserControl.DisplayName = "ПользовательскийЭлементУправления (UserControl)";
            listBox1.Items.Add(toolUserControl);

            ToolboxItem toolVScrollBar = new ToolboxItem(typeof(VScrollBar));
            toolVScrollBar.DisplayName = "ВертикальнаяПрокрутка (VScrollBar)";
            listBox1.Items.Add(toolVScrollBar);

            listBox1.Sorted = true;
            listBox1.HorizontalScrollbar = true;
            IpDesignerCore.Toolbox = listBox1;
        }

        private void _form_Click(object sender, EventArgs e)
        {
            pDesigner.SplitterpDesigner.Visible = true;
            pDesigner.CodePanel.Visible = false;
            _addForm.Enabled = true; // "Добавить Форму"
            _deleteForm.Enabled = true; // "Удалить Форму"
            _edit.Enabled = true; // "Правка"
            _tools.Enabled = true; // "Инструменты"
            pDesigner.SplitterpDesigner.Panel2Collapsed = false;
            pnl4Toolbox.Visible = true;
            _form.Enabled = false;
            _code.Enabled = true;
            _form.CheckState = System.Windows.Forms.CheckState.Checked;
            _code.CheckState = System.Windows.Forms.CheckState.Unchecked;
        }

        private void _code_Click(object sender, EventArgs e)
        {
            SaveScript.comps.Clear();
            pDesigner.SplitterpDesigner.Visible = false;
            pDesigner.CodePanel.Visible = true;
            _addForm.Enabled = false; // "Добавить Форму"
            _deleteForm.Enabled = false; // "Удалить Форму"
            _edit.Enabled = false; // "Правка"
            _tools.Enabled = false; // "Инструменты"
            pDesigner.SplitterpDesigner.Panel2Collapsed = true;
            pnl4Toolbox.Visible = false;
            _form.Enabled = true;
            _code.Enabled = false;
            _form.CheckState = System.Windows.Forms.CheckState.Unchecked;
            _code.CheckState = System.Windows.Forms.CheckState.Checked;

            string scriptText = SaveScript.GetScriptText();
            if ((bool)Settings.Default["visualSyleForms"])
            {
                string strFind = @"Ф = Новый ФормыДляОдноСкрипта();";
                string strReplace = @"Ф = Новый ФормыДляОдноСкрипта();" + Environment.NewLine +
                @"    Ф.ВключитьВизуальныеСтили();";
                scriptText = scriptText.Replace(strFind, strReplace);
            }
            pDesigner.RichTextBox.Text = scriptText;
        }

        private void _generateScript_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Filter = "OS files(*.os)|*.os|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            SaveScript.comps.Clear();

            string scriptText = SaveScript.GetScriptText(saveFileDialog1.FileName);
            if ((bool)Settings.Default["visualSyleForms"])
            {
                string strFind = @"Ф = Новый ФормыДляОдноСкрипта();";
                string strReplace = @"Ф = Новый ФормыДляОдноСкрипта();" + Environment.NewLine +
                @"    Ф.ВключитьВизуальныеСтили();";
                scriptText = scriptText.Replace(strFind, strReplace);
            }

            File.WriteAllText(saveFileDialog1.FileName, scriptText, Encoding.UTF8);
            //File.WriteAllText("C:\\444\\Проба.os", SaveScript.GetScriptText("C:\\444\\"), Encoding.UTF8);
        }

        private void _run_Click(object sender, EventArgs e)
        {
            OneScriptFormsDesigner.PropertyGrid.Refresh();
            string Script = SaveScript.GetScriptText();
            if (!(bool)Settings.Default["styleScript"])
            {
                string strFind = @"#КонецОбласти";
                string strReplace = @"#КонецОбласти" + Environment.NewLine + 
"ПодключитьВнешнююКомпоненту(" + "\u0022" + Settings.Default["dllPath"] + "\u0022" + @");" + Environment.NewLine +
@"Ф = Новый ФормыДляОдноСкрипта();
ПриСозданииФормы(Ф.Форма());
Ф.ЗапуститьОбработкуСобытий();";
                Script = Script.Replace(strFind, strReplace);
            }
            if ((bool)Settings.Default["visualSyleForms"])
            {
                string strFind = @"Ф = Новый ФормыДляОдноСкрипта();";
                string strReplace = @"Ф = Новый ФормыДляОдноСкрипта();" + Environment.NewLine +
                @"    Ф.ВключитьВизуальныеСтили();";
                Script = Script.Replace(strFind, strReplace);
            }
            string strTempFile = String.Format(Path.GetTempPath() + "oscript_{0}_{1}.os", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString().Replace("-", ""));
            File.WriteAllText(strTempFile, Script, Encoding.UTF8);

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.Arguments = strTempFile;
            psi.FileName = (string)Settings.Default["osPath"];
            System.Diagnostics.Process.Start(psi);
        }

        private void _settings_Click(object sender, EventArgs e)
        {
            settingsForm = new MySettingsForm();

            if (settingsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Записываем значения в Settings.
                Settings.Default["osPath"] = settingsForm.OSPath;
                Settings.Default["dllPath"] = settingsForm.DLLPath;
                Settings.Default["styleScript"] = settingsForm.StyleScript;
                Settings.Default["visualSyleDesigner"] = settingsForm.SyleDesigner;
                Settings.Default["visualSyleForms"] = settingsForm.SyleForms;
                Settings.Default.Save();
            }
        }

        private void _exit_Click(object sender, EventArgs e)
        {
            //* 18.12.2021 perfolenta
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
            //***
        }

        private void _saveForm_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Filter = "OSD files(*.osd)|*.osd|All files(*.*)|*.*";
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
            if (File.Exists(savedForm.Path))
            {
                File.WriteAllText(savedForm.Path, SaveForm.GetScriptText(saveFileDialog1.FileName), Encoding.UTF8);
            }
            else
            {
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                savedForm.Path = saveFileDialog1.FileName;
                File.WriteAllText(saveFileDialog1.FileName, SaveForm.GetScriptText(saveFileDialog1.FileName), Encoding.UTF8);
                //File.WriteAllText("C:\\444\\Форма1сохран\\Форма1сохран.osd", SaveForm.GetScriptText("C:\\444\\Форма1сохран\\"), Encoding.UTF8);
            }

            // Запомним состояние дизайнера с этой формой.
            OneScriptFormsDesigner.DesignSurfaceState(true);
        }

        private void _saveFormAs_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Filter = "OSD files(*.osd)|*.osd|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            ((Form)OneScriptFormsDesigner.DesignerHost.RootComponent).Path = saveFileDialog1.FileName;
            File.WriteAllText(saveFileDialog1.FileName, SaveForm.GetScriptText(saveFileDialog1.FileName), Encoding.UTF8);
            //File.WriteAllText("C:\\444\\Форма1сохран\\Форма1сохран.osd", SaveForm.GetScriptText("C:\\444\\Форма1сохран\\"), Encoding.UTF8);

            // Запомним состояние дизайнера с этой формой.
            OneScriptFormsDesigner.DesignSurfaceState(true);
        }

        private void _loadForm_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            OpenFileDialog1.RestoreDirectory = true;
            OpenFileDialog1.Filter = "OSD files(*.osd)|*.osd|All files(*.*)|*.*";
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string strFile = File.ReadAllText(OpenFileDialog1.FileName);
            //string strOSDBefore = File.ReadAllText("C:\\444\\Форма1сохран\\Форма1сохран.osd");

            OneScriptFormsDesigner.block2 = true;

            string[] result = null;
            string[] stringSeparators = new string[] { Environment.NewLine };
            string ComponentBlok = null;
            string rootBlok = null;

            string strOSD = "";
            result = strFile.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Length; i++)
            {
                string strres = result[i];
                if (strres.Contains(".ВыбранныйПуть = \u0022") || 
                    strres.Contains(".Заголовок = \u0022") || 
                    strres.Contains(".ИмяФайла = \u0022") || 
                    strres.Contains(".НачальныйКаталог = \u0022") || 
                    strres.Contains(".Описание = \u0022") || 
                    strres.Contains(".ПолныйПуть = \u0022") || 
                    strres.Contains(".ПользовательскийФормат = \u0022") || 
                    strres.Contains(".Путь = \u0022") || 
                    strres.Contains(".РазделительПути = \u0022") || 
                    strres.Contains(".Текст = \u0022") || 
                    strres.Contains(".ТекстЗаголовка = \u0022") || 
                    strres.Contains(".ТекстПодсказки = \u0022") || 
                    strres.Contains(".Фильтр = \u0022")
                    )
                {
                    string strBefore = OneScriptFormsDesigner.ParseBetween(strres, null, " = \u0022");
                    string strAfter = OneScriptFormsDesigner.ParseBetween(strres, "= \u0022", null);
                    strOSD = strOSD + strBefore.Replace(" ", "") + "=\u0022" + strAfter + Environment.NewLine;
                }
                else
                {
                    strOSD = strOSD + strres.Replace(" ", "") + Environment.NewLine;
                }
            }
            result = null;

            strOSD = strOSD.Trim();

            // Соберем из блока конструкторов имена компонентов в CompNames.
            List<string> CompNames = new List<string>();
            Dictionary<string, object> dictObjects = new Dictionary<string, object>(); // Словарь для соответствия имени переменной в скрипте объекту в библиотеке.
            string ConstructorBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<Конструкторы]", @"[Конструкторы>]");
            result = ConstructorBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Length; i++)
            {
                string s = OneScriptFormsDesigner.ParseBetween(result[i], null, @"=Ф.");
                if (s != null)
                {
                    if (s.Substring(0, 2) != @"//")
                    {
                        CompNames.Add(s);
                        dictObjects.Add(s, null);
                    }
                }
            }
            result = null;

            // Добавим вкладку и создадим на ней загружаемую форму.
            DesignSurfaceExt2 var1 = IpDesignerCore.AddDesignSurface<Form>(670, 600, AlignmentModeEnum.SnapLines, new Size(1, 1), CompNames[0]);
            Component rootComponent = (Component)var1.ComponentContainer.Components[0];
            ((Form)rootComponent).Path = OpenFileDialog1.FileName;

            dictObjects[CompNames[0]] = rootComponent;

            string formName = CompNames[0];
            rootComponent.GetType().GetProperty("Text").SetValue(rootComponent, formName);
            rootBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<" + formName + @"]", @"[" + formName + @">]");
            if (rootBlok != null)
            {
                // Установим для формы свойства.
                result = rootBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                for (int i1 = 0; i1 < result.Length; i1++)
                {
                    string strCurrent = result[i1];
                    if (strCurrent.Length >= 2)
                    {
                        if (strCurrent.Substring(0, 2) != @"//")
                        {
                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, formName + ".", "=");
                            if (displayName != "КнопкаОтмена" && displayName != "КнопкаПринять" && !strCurrent.StartsWith("Подсказка"))
                            {
                                string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                PropValueConverter.SetPropValue(rootComponent, displayName, strPropertyValue);
                            }
                        }
                    }
                }
                propertyGrid1.Refresh();
                result = null;
            }

            // Создадим остальные компоненты но пока не устанавливаем для них свойства, так как могут быть не все родители созданы.
            IDesignSurfaceExt surface = OneScriptFormsDesigner.ActiveDesignSurface;
            for (int i = 1; i < CompNames.Count; i++)
            {
                string componentName = CompNames[i];
                string type_NameRu = componentName;
                for (int i1 = 0; i1 < 10; i1++)
                {
                    type_NameRu = type_NameRu.Replace(i1.ToString(), "");
                }

                string type_NameEn = "osfDesigner." + OneScriptFormsDesigner.namesRuEn[type_NameRu];
                Type type = Type.GetType(type_NameEn);

                if (type == typeof(osfDesigner.ImageList))
                {
                    ToolboxItem toolImageList1 = new ToolboxItem(typeof(System.Windows.Forms.ImageList));
                    Component comp1 = (Component)toolImageList1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Для comp1 уже создан дублер, получим его.
                    osfDesigner.ImageList SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    dictObjects[componentName] = SimilarObj;

                    SimilarObj.DefaultValues = @"ГлубинаЦвета == Глубина8
Изображения == (Коллекция)
РазмерИзображения == {Ширина=16, Высота=16}
(Name) == " + comp1.Site.Name + Environment.NewLine;
                }
                else if (type == typeof(osfDesigner.MainMenu))
                {
                    ToolboxItem toolMainMenu1 = new ToolboxItem(typeof(System.Windows.Forms.MainMenu));
                    Component comp1 = (Component)toolMainMenu1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Для comp1 уже создан дублер, получим его.
                    osfDesigner.MainMenu SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    dictObjects[componentName] = SimilarObj;

                    SimilarObj.FrmMenuItems = new frmMenuItems(SimilarObj);
                }
                else if (type == typeof(osfDesigner.TabPage))
                {
                    ToolboxItem toolTabPage1 = new ToolboxItem(typeof(System.Windows.Forms.TabPage));
                    Component comp1 = (Component)toolTabPage1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Для comp1 уже создан дублер, получим его.
                    osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    SimilarObj.OriginalObj = (System.Windows.Forms.TabPage)comp1;
                    OneScriptFormsDesigner.PassProperties(comp1, SimilarObj); // Передадим свойства.
                    dictObjects[componentName] = SimilarObj;

                    OneScriptFormsDesigner.PropertyGrid.SelectedObject = SimilarObj;
                    SimilarObj.DefaultValues = OneScriptFormsDesigner.GetDefaultValues(SimilarObj, OneScriptFormsDesigner.PropertyGrid);
                }
                else if (type == typeof(osfDesigner.TabControl))
                {
                    ToolboxItem toolTabControl1 = new ToolboxItem(typeof(osfDesigner.TabControl));
                    Component comp1 = (Component)toolTabControl1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Удалим две вкладки, которые дизайнер для панели вкладок создает автоматически.
                    IDesignerEventService des = (IDesignerEventService)pDesigner.DSME.GetService(typeof(IDesignerEventService));
                    if (des != null)
                    {
                        for (int i1 = 0; i1 < ((osfDesigner.TabControl)comp1).TabPages.Count; i1++)
                        {
                            des.ActiveDesigner.Container.Remove(((osfDesigner.TabControl)comp1).TabPages[i1]);
                        }
                        ((osfDesigner.TabControl)comp1).TabPages.Clear();
                    }
                    dictObjects[componentName] = comp1;
                }

                else if (type == typeof(osfDesigner.FileSystemWatcher) ||
                    type == typeof(osfDesigner.FolderBrowserDialog) ||
                    type == typeof(osfDesigner.ColorDialog) ||
                    type == typeof(osfDesigner.FontDialog) ||
                    type == typeof(osfDesigner.OpenFileDialog) ||
                    type == typeof(osfDesigner.SaveFileDialog) ||
                    type == typeof(osfDesigner.NotifyIcon) ||
                    type == typeof(osfDesigner.ToolTip) ||
                    type == typeof(osfDesigner.Timer))
                {
                    ToolboxItem toolComp1 = new ToolboxItem(type);
                    Component comp1 = (Component)toolComp1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    dictObjects[componentName] = comp1;
                }
                else
                {
                    Component control1 = surface.CreateControl(type, new Size(200, 20), new Point(10, 200));
                    dictObjects[componentName] = control1;
                }
                ((Component)dictObjects[componentName]).Site.Name = componentName;
            }

            // Установим свойства компонентов.
            for (int i = 1; i < CompNames.Count; i++)
            {
                string componentName = CompNames[i];
                Component control = (Component)dictObjects[componentName];
                ComponentBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<" + componentName + @"]", @"[" + componentName + @">]");
                if (ComponentBlok != null)
                {
                    result = ComponentBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    for (int i1 = 0; i1 < result.Length; i1++)
                    {
                        string strCurrent = result[i1];
                        if (strCurrent.Length >= 2)
                        {
                            if (strCurrent.Substring(0, 2) != @"//")
                            {
                                if (componentName.Contains("СписокИзображений"))
                                {
                                    if (strCurrent.Contains("="))
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                    }
                                    else
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", ".");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "Ф.Картинка(\u0022", "\u0022)");
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                    }
                                }
                                else if (componentName.Contains("Календарь"))
                                {
                                    if (strCurrent.Contains("="))
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                    else
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", ".");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "Дата(", "))");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                }
                                else if (componentName.Contains("ГлавноеМеню"))
                                {
                                    string controlName = ((osfDesigner.MainMenu)control).Name;
                                    if (strCurrent.Contains(".ЭлементыМеню.Добавить(Ф.ЭлементМеню("))
                                    {
                                        osfDesigner.MainMenu MainMenu1 = (osfDesigner.MainMenu)control;
                                        System.Windows.Forms.TreeView TreeView1 = MainMenu1.TreeView;
                                        string Text = OneScriptFormsDesigner.ParseBetween(strCurrent, "(\u0022", "\u0022)");
                                        string Name = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        if (strCurrent.Contains(componentName + ".")) // Создаем элемент главного меню.
                                        {
                                            if (!Name.Contains("Сепаратор"))
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.Name = Name;
                                                MenuItemEntry1.Text = Text;
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == " + Name + Environment.NewLine + 
@"ТипСлияния == Добавить
(Name) == " + Name + Environment.NewLine;
                                                MainMenu1.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                                                OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
                                                TreeNode TreeNode1 = new TreeNode();
                                                TreeNode1.Tag = MenuItemEntry1;
                                                TreeNode1.Text = MenuItemEntry1.Text;
                                                TreeView1.Nodes.Add(TreeNode1);
                                                TreeView1.SelectedNode = TreeNode1;
                                            }
                                            else
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.Name = Name;
                                                MenuItemEntry1.Text = Text;
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == " + Name + Environment.NewLine +
@"ТипСлияния == Добавить
(Name) == " + Name + Environment.NewLine;
                                                MainMenu1.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                                                OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);

                                                TreeNode TreeNode1 = new TreeNode();
                                                TreeNode1.Tag = MenuItemEntry1;
                                                TreeNode1.Text = MenuItemEntry1.Text;
                                                TreeView1.Nodes.Add(TreeNode1);
                                                TreeView1.SelectedNode = TreeNode1;

                                                // Свойство Checked у родителя нужно установить в false.
                                                if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
                                                {
                                                    ((MenuItem)MenuItemEntry1.Parent).Checked = false;
                                                }
                                                TreeView1.SelectedNode.Expand();
                                            }
                                        }
                                        else // Создаем элемент подменю.
                                        {
                                            if (!Name.Contains("Сепаратор"))
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.Name = Name;
                                                MenuItemEntry1.Text = Text;
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == " + Name + Environment.NewLine +
@"ТипСлияния == Добавить
(Name) == " + Name + Environment.NewLine;

                                                string nameNodeParent = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ".");
                                                TreeNode SelectedNode = null;
                                                SelectedNodeSearch(TreeView1, nameNodeParent, ref SelectedNode, null);
                                                TreeView1.SelectedNode = SelectedNode;

                                                MenuItemEntry MenuItemParent = (MenuItemEntry)TreeView1.SelectedNode.Tag;
                                                MenuItemParent.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                                                OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
                                                TreeNode TreeNode1 = new TreeNode();
                                                TreeNode1.Tag = MenuItemEntry1;
                                                TreeNode1.Text = MenuItemEntry1.Text;
                                                TreeView1.SelectedNode.Nodes.Add(TreeNode1);

                                                // Свойство Checked у родителя нужно установить в false.
                                                if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
                                                {
                                                    ((MenuItem)MenuItemEntry1.Parent).Checked = false;
                                                }
                                                TreeView1.SelectedNode = TreeNode1;
                                            }
                                            else
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.Name = Name;
                                                MenuItemEntry1.Text = Text;
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == " + Name + Environment.NewLine +
@"ТипСлияния == Добавить
(Name) == " + Name + Environment.NewLine;
                                                string nameNodeParent = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ".");

                                                TreeNode SelectedNode = null;
                                                SelectedNodeSearch(TreeView1, nameNodeParent, ref SelectedNode, null);
                                                TreeView1.SelectedNode = SelectedNode;

                                                MenuItemEntry MenuItemParent = (MenuItemEntry)TreeView1.SelectedNode.Tag;
                                                MenuItemParent.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                                                OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);

                                                TreeNode TreeNode1 = new TreeNode();
                                                TreeNode1.Tag = MenuItemEntry1;
                                                TreeNode1.Text = MenuItemEntry1.Text;
                                                TreeView1.SelectedNode.Nodes.Add(TreeNode1);

                                                // Свойство Checked у родителя нужно установить в false.
                                                if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
                                                {
                                                    ((MenuItem)MenuItemEntry1.Parent).Checked = false;
                                                }
                                                TreeView1.SelectedNode.Expand();
                                            }
                                        }
                                    }
                                    else // Обрабатываем как свойство элемента меню.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = (object)dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, null);
                                    }
                                }
                                else if (control.GetType() == typeof(osfDesigner.FileSystemWatcher) ||
                                    control.GetType() == typeof(osfDesigner.FolderBrowserDialog) ||
                                    control.GetType() == typeof(osfDesigner.ColorDialog) ||
                                    control.GetType() == typeof(osfDesigner.FontDialog) ||
                                    control.GetType() == typeof(osfDesigner.OpenFileDialog) ||
                                    control.GetType() == typeof(osfDesigner.SaveFileDialog) ||
                                    control.GetType() == typeof(osfDesigner.NotifyIcon) ||
                                    control.GetType() == typeof(osfDesigner.ToolTip) ||
                                    control.GetType() == typeof(osfDesigner.Timer))
                                {
                                    string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                    string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                }
                                else if (strCurrent.StartsWith("Подсказка"))
                                {
                                    string displayName = "ToolTip на " + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".УстановитьПодсказку");
                                    string strPropertyValue = strCurrent;
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                }
                                else if (componentName.Contains("Вкладка"))
                                {
                                    string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                    string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                    string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                    Control parent = (Control)dictObjects[parentName];
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                }
                                else if (componentName.Contains("СтрокаСостояния"))
                                {
                                    string controlName = ((osfDesigner.StatusBar)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("СтрокаСостояния") && !header.Contains("Панель"))
                                    {
                                        if (strCurrent.Contains(".Панели.Добавить(")) // Добавляем панель.
                                        {
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            StatusBarPanel StatusBarPanel1 = (StatusBarPanel)dictObjects[nameItem];
                                            ((osfDesigner.StatusBar)control).Panels.Add(StatusBarPanel1);
                                        }
                                        else // Обрабатываем как свойство строки состояния.
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                        }
                                    }
                                    else 
                                    {
                                        if (strCurrent.Contains("Ф.ПанельСтрокиСостояния();")) // Создаем панель.
                                        {
                                            StatusBarPanel StatusBarPanel1 = new StatusBarPanel();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            StatusBarPanel1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, StatusBarPanel1);

                                            StatusBarPanel1.DefaultValues = @"АвтоРазмер == Отсутствие
Значок == 
СтильГраницы == Утопленная
Текст == 
Ширина == 100
МинимальнаяШирина == 10
(Name) == 
";
                                        }
                                        else // Обрабатываем как свойство панели строки состояния.
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                            string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            object control2 = dictObjects[nameObj];
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, null);
                                        }
                                    }
                                }
                                else if (componentName.Contains("ПанельИнструментов"))
                                {
                                    string controlName = ((osfDesigner.ToolBar)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (strCurrent.Contains(".Кнопки.Добавить(")) // Добавляем кнопку панели инструментов.
                                    {
                                        System.Windows.Forms.ToolBarButton OriginalObj = new System.Windows.Forms.ToolBarButton();
                                        osfDesigner.ToolBarButton SimilarObj = new osfDesigner.ToolBarButton();
                                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                                        SimilarObj.OriginalObj = OriginalObj;
                                        SimilarObj.Parent = OriginalObj.Parent;
                                        SimilarObj.Style = (osfDesigner.ToolBarButtonStyle)OriginalObj.Style;
                                        OriginalObj.Tag = SimilarObj;

                                        string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        SimilarObj.Name = nameItem;
                                        ((osfDesigner.ToolBar)control).Buttons.Add(OriginalObj);
                                        dictObjects.Add(controlName + nameItem, SimilarObj);

                                        SimilarObj.DefaultValues = @"Доступность == Истина
ИндексИзображения == -1
Нажата == Ложь
НейтральноеПоложение == Ложь
Отображать == Истина
Прямоугольник == 
Стиль == СтандартнаяТрехмерная
Текст == 
ТекстПодсказки == 
(Name) == ";
                                    }
                                    else if (!header.Contains("Кн")) // Обрабатываем как свойство панели инструментов.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                    else // Обрабатываем как свойство кнопки панели инструментов.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, null);
                                    }
                                }
                                else if (componentName.Contains("СписокЭлементов"))
                                {
                                    string controlName = ((osfDesigner.ListView)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    string fragment1 = OneScriptFormsDesigner.ParseBetween(header, "СписокЭлементов", null);
                                    fragment1 = fragment1.Replace("Колонка", "флажок").Replace("Элемент", "флажок").Replace("Подэлемент", "флажок");
                                    if (header.Contains("СписокЭлементов") && !fragment1.Contains("флажок")) // Обрабатываем как свойство списка элементов.
                                    {
                                        if (strCurrent.Contains(".Элементы.Добавить(")) // Добавляем элемент списка элементов.
                                        {
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            ListViewItem ListViewItem1 = (ListViewItem)dictObjects[nameItem];
                                            ((osfDesigner.ListView)control).Items.Add(ListViewItem1);
                                        }
                                        else if (strCurrent.Contains(".Колонки.Добавить(")) // Добавляем колонку списка элементов.
                                        {
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            ColumnHeader ColumnHeader1 = (ColumnHeader)dictObjects[nameItem];
                                            ((osfDesigner.ListView)control).Columns.Add(ColumnHeader1);
                                        }
                                        else // Обрабатываем как свойство для СписокЭлементов.
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                        }
                                    }
                                    else
                                    {
                                        if (strCurrent.Contains("Ф.ЭлементСпискаЭлементов();")) // Создаем элемент списка элементов.
                                        {
                                            ListViewItem ListViewItem1 = new ListViewItem();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            ListViewItem1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, ListViewItem1);

                                            ListViewItem1.DefaultValues = @"ИспользоватьСтильДляПодэлементов == Истина
ОсновнойЦвет == ТекстОкна
Помечен == Ложь
Текст == 
ЦветФона == Окно
Шрифт == Microsoft Sans Serif; 7,8pt
Подэлементы == (Коллекция)
ИндексИзображения == -1
(Name) == ";
                                        }
                                        else if (strCurrent.Contains("Ф.ПодэлементСпискаЭлементов();")) // Создаем подэлемент списка элементов.
                                        {
                                            ListViewSubItem ListViewSubItem1 = new ListViewSubItem();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            ListViewSubItem1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, ListViewSubItem1);

                                            ListViewSubItem1.DefaultValues = @"ОсновнойЦвет == ТекстОкна
Текст == 
ЦветФона == Окно
Шрифт == Microsoft Sans Serif; 7,8pt
(Name) == ";
                                        }
                                        else if (strCurrent.Contains("Ф.Колонка();")) // Создаем колонку списка элементов.
                                        {
                                            ColumnHeader ColumnHeader1 = new ColumnHeader();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            ColumnHeader1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, ColumnHeader1);

                                            ColumnHeader1.DefaultValues = @"ВыравниваниеТекста == Лево
Текст == 
ТипСортировки == Текст
Ширина == 60
(Name) == ";
                                        }
                                        else if (strCurrent.Contains(".Подэлементы.Добавить(")) // Добавляем подэлемент списка элементов.
                                        {
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            ListViewItem ListViewItem1 = (osfDesigner.ListViewItem)dictObjects[nameItem];
                                            string nameSubItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            ListViewSubItem ListViewSubItem1 = (osfDesigner.ListViewSubItem)dictObjects[nameSubItem];
                                            ListViewItem1.SubItems.Add(ListViewSubItem1);
                                        }
                                        else // Обрабатываем как свойство для элемента или подэлемента СписокЭлементов.
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                            string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            object control2 = dictObjects[nameObj];
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, null);
                                        }
                                    }
                                }
                                else if (componentName.Contains("ПолеСписка"))
                                {
                                    if (strCurrent.Contains(".Элементы.Добавить(Ф.ЭлементСписка(")) // Добавляем элемент поля списка.
                                    {
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "(Ф.ЭлементСписка", ");");
                                        string itemText = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "(", ",").Replace("\u0022", "");
                                        string itemValue = OneScriptFormsDesigner.ParseBetween(strPropertyValue, ",", ")");

                                        osfDesigner.ListItemListBox ListItemListBox1 = new ListItemListBox();
                                        ListItemListBox1.Text = itemText;

                                        if (itemValue.StartsWith("\u0022") && itemValue.EndsWith("\u0022")) // Тип Строка.
                                        {
                                            itemValue = itemValue.Replace("\u0022", "");
                                            ListItemListBox1.Value = itemValue;
                                            ListItemListBox1.ValueType = DataType.Строка;
                                        }
                                        else if (strPropertyValue.Contains("Ложь") || strPropertyValue.Contains("Истина")) // Тип Булево.
                                        {
                                            ListItemListBox1.Value = true;
                                            ListItemListBox1.ValueType = DataType.Булево;
                                            if (itemValue == "Ложь")
                                            {
                                                ListItemListBox1.Value = false;
                                            }
                                        }
                                        else if (strPropertyValue.Contains("Дата")) // Тип Дата.
                                        {
                                            DateTime rez1 = new DateTime();
                                            string[] result1 = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "Дата(", "))").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i2 = 0; i2 < result1.Length; i2++)
                                            {
                                                if (i2 == 0)
                                                {
                                                    rez1 = rez1.AddYears(Int32.Parse(result1[0]) - 1);
                                                }
                                                if (i2 == 1)
                                                {
                                                    rez1 = rez1.AddMonths(Int32.Parse(result1[1]) - 1);
                                                }
                                                if (i2 == 2)
                                                {
                                                    rez1 = rez1.AddDays(Int32.Parse(result1[2]) - 1);
                                                }
                                                if (i2 == 3)
                                                {
                                                    rez1 = rez1.AddHours(Int32.Parse(result1[3]));
                                                }
                                                if (i2 == 4)
                                                {
                                                    rez1 = rez1.AddMinutes(Int32.Parse(result1[4]));
                                                }
                                                if (i2 == 5)
                                                {
                                                    rez1 = rez1.AddSeconds(Int32.Parse(result1[5]));
                                                }
                                            }
                                            ListItemListBox1.Value = rez1;
                                            ListItemListBox1.ValueType = DataType.Дата;
                                        }
                                        else // Тип Число.
                                        {
                                            ListItemListBox1.Value = Int32.Parse(itemValue);
                                            ListItemListBox1.ValueType = DataType.Число;
                                        }
                                        ((ListBox)control).Items.Add(ListItemListBox1);
                                    }
                                    else // Обрабатываем как свойство поля списка.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                }
                                else if (componentName.Contains("ПолеВыбора"))
                                {
                                    if (strCurrent.Contains(".Элементы.Добавить(Ф.ЭлементСписка(")) // Добавляем элемент поля выбора.
                                    {
                                        // Определяем тип элемента списка и создаем его.
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "(Ф.ЭлементСписка", ");");
                                        string itemText = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "(", ",").Replace("\u0022", "");
                                        string itemValue = OneScriptFormsDesigner.ParseBetween(strPropertyValue, ",", ")");

                                        osfDesigner.ListItemComboBox ListItemComboBox1 = new ListItemComboBox();
                                        ListItemComboBox1.Text = itemText;

                                        if (itemValue.StartsWith("\u0022") && itemValue.EndsWith("\u0022")) // Тип Строка.
                                        {
                                            itemValue = itemValue.Replace("\u0022", "");
                                            ListItemComboBox1.Value = itemValue;
                                            ListItemComboBox1.ValueType = DataType.Строка;
                                        }
                                        else if (strPropertyValue.Contains("Ложь") || strPropertyValue.Contains("Истина")) // Тип Булево.
                                        {
                                            ListItemComboBox1.Value = true;
                                            ListItemComboBox1.ValueType = DataType.Булево;
                                            if (itemValue == "Ложь")
                                            {
                                                ListItemComboBox1.Value = false;
                                            }
                                        }
                                        else if (strPropertyValue.Contains("Дата")) // Тип Дата.
                                        {
                                            DateTime rez1 = new DateTime();
                                            string[] result1 = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "Дата(", "))").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i2 = 0; i2 < result1.Length; i2++)
                                            {
                                                if (i2 == 0)
                                                {
                                                    rez1 = rez1.AddYears(Int32.Parse(result1[0]) - 1);
                                                }
                                                if (i2 == 1)
                                                {
                                                    rez1 = rez1.AddMonths(Int32.Parse(result1[1]) - 1);
                                                }
                                                if (i2 == 2)
                                                {
                                                    rez1 = rez1.AddDays(Int32.Parse(result1[2]) - 1);
                                                }
                                                if (i2 == 3)
                                                {
                                                    rez1 = rez1.AddHours(Int32.Parse(result1[3]));
                                                }
                                                if (i2 == 4)
                                                {
                                                    rez1 = rez1.AddMinutes(Int32.Parse(result1[4]));
                                                }
                                                if (i2 == 5)
                                                {
                                                    rez1 = rez1.AddSeconds(Int32.Parse(result1[5]));
                                                }
                                            }
                                            ListItemComboBox1.Value = rez1;
                                            ListItemComboBox1.ValueType = DataType.Дата;
                                        }
                                        else // Это тип Число.
                                        {
                                            ListItemComboBox1.Value = Int32.Parse(itemValue);
                                            ListItemComboBox1.ValueType = DataType.Число;
                                        }
                                        ((ComboBox)control).Items.Add(ListItemComboBox1);
                                    }
                                    else // Обрабатываем как свойство поля выбора.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                }
                                else if (componentName.Contains("СеткаДанных"))
                                {
                                    string controlName = ((osfDesigner.DataGrid)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("СеткаДанных") && !header.Contains("Стиль")) // Обрабатываем как свойство сетки данных.
                                    {
                                        if (!strCurrent.Contains(".СтилиТаблицы.Добавить("))
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                        }
                                    }
                                    else
                                    {
                                        if (strCurrent.Contains("Ф.СтильТаблицыСеткиДанных();"))
                                        {
                                            osfDesigner.DataGridTableStyle SimilarObj = new osfDesigner.DataGridTableStyle();
                                            System.Windows.Forms.DataGridTableStyle OriginalObj = new System.Windows.Forms.DataGridTableStyle();
                                            SimilarObj.OriginalObj = OriginalObj;
                                            OneScriptFormsDesigner.AddToDictionary(OriginalObj, SimilarObj);
                                            OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                                            string nameStyle = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            SimilarObj.NameStyle = nameStyle;
                                            ((osfDesigner.DataGrid)control).TableStyles.Add(OriginalObj);
                                            dictObjects.Add(controlName + nameStyle, SimilarObj);

                                            SimilarObj.DefaultValues = @"ШрифтЗаголовков == Microsoft Sans Serif; 7,8pt
ПредпочтительнаяВысотаСтрок == 18
ПредпочтительнаяШиринаСтолбцов == 75
ШиринаЗаголовковСтрок == 35
РазрешитьСортировку == Истина
ОтображатьЗаголовкиСтолбцов == Истина
ОтображатьЗаголовкиСтрок == Истина
ИмяОтображаемого == 
СтилиКолонкиСеткиДанных == (Коллекция)
Текст == 
ТолькоЧтение == Ложь
ОсновнойЦвет == ТекстОкна
ОсновнойЦветЗаголовков == ТекстЭлемента
ЦветСетки == ЛицеваяЭлемента
ЦветФона == Окно
ЦветФонаЗаголовков == ЛицеваяЭлемента
ЦветФонаНечетныхСтрок == Окно";
                                        }
                                        else if (strCurrent.Contains("Ф.СтильКолонкиБулево();"))
                                        {
                                            string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            DataGridBoolColumn DataGridBoolColumn1 = new DataGridBoolColumn();
                                            dictObjects.Add(controlName + nameObj, DataGridBoolColumn1);
                                            DataGridBoolColumn1.NameStyle = nameObj;

                                            DataGridBoolColumn1.DefaultValues = @"Ширина == 75
Выравнивание == Лево
ТекстЗаголовка == 
ИмяОтображаемого == 
Текст == 
ТолькоЧтение == Ложь";
                                        }
                                        else if (strCurrent.Contains("Ф.СтильКолонкиПолеВвода();"))
                                        {
                                            string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            DataGridTextBoxColumn DataGridTextBoxColumn1 = new DataGridTextBoxColumn();
                                            dictObjects.Add(controlName + nameObj, DataGridTextBoxColumn1);
                                            DataGridTextBoxColumn1.NameStyle = nameObj;

                                            DataGridTextBoxColumn1.DefaultValues = @"Ширина == 75
Выравнивание == Лево
ТекстЗаголовка == 
ДвойноеНажатие == 
ИмяОтображаемого == 
Текст == 
ТолькоЧтение == Ложь";
                                        }
                                        else if (strCurrent.Contains("Ф.СтильКолонкиПолеВыбора();"))
                                        {
                                            string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            DataGridComboBoxColumnStyle DataGridComboBoxColumnStyle1 = new DataGridComboBoxColumnStyle();
                                            dictObjects.Add(controlName + nameObj, DataGridComboBoxColumnStyle1);
                                            DataGridComboBoxColumnStyle1.NameStyle = nameObj;

                                            DataGridComboBoxColumnStyle1.DefaultValues = @"Ширина == 75
Выравнивание == Лево
ТекстЗаголовка == 
ИмяОтображаемого == 
Текст == 
ТолькоЧтение == Ложь";
                                        }
                                        else if (strCurrent.Contains(".СтилиКолонкиСеткиДанных.Добавить("))
                                        {
                                            string nameTableStyle = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            osfDesigner.DataGridTableStyle tableStyle = (osfDesigner.DataGridTableStyle)dictObjects[nameTableStyle];
                                            string nameColumnStyle = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            Component columnStyle = (Component)dictObjects[nameColumnStyle];
                                            tableStyle.OriginalObj.GridColumnStyles.Add((DataGridColumnStyle)columnStyle);
                                        }
                                        else
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                            string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            Component control2 = (Component)dictObjects[nameObj];
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, null);
                                        }
                                    }
                                }
                                else if (componentName.Contains("Дерево"))
                                {
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("Дерево") && !header.Contains("Узел")) // Обрабатываем как свойство дерева.
                                    {
                                        if (strCurrent.Contains("Узлы"))
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", ".");
                                            PropValueConverter.SetPropValue(control, displayName, strCurrent, null);
                                        }
                                        else
                                        {

                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                        }
                                    }
                                    else // Обрабатываем как свойство узла.
                                    {
                                        if (strCurrent.Contains("Узлы"))
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", ".");
                                            PropValueConverter.SetPropValue(control, displayName, strCurrent, null);
                                        }
                                        else
                                        {
                                            string displayName = "Узлы";
                                            PropValueConverter.SetPropValue(control, displayName, strCurrent, null);
                                        }
                                    }
                                }
                                else
                                {
                                    string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                    string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                    string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                    Control parent = (Control)dictObjects[parentName];

                                    if (parent.GetType() == typeof(osfDesigner.TabPage))
                                    {
                                        parent = OneScriptFormsDesigner.RevertOriginalObj(parent);
                                    }

                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                }
                            }
                        }
                    }

                    if (control.GetType() == typeof(osfDesigner.ToolBar) || 
                        control.GetType() == typeof(osfDesigner.Splitter) || 
                        control.GetType() == typeof(osfDesigner.StatusBar))
                    {
                        ((Control)control).BringToFront();
                    }

                    result = null;
                    ComponentBlok = null;
                    propertyGrid1.Refresh();
                }

                if (componentName.Contains("ГлавноеМеню"))
                {
                    OneScriptFormsDesigner.PassProperties(control, OneScriptFormsDesigner.RevertOriginalObj(control)); // Передадим свойства.
                }
            }

            // Если для формы заданы КнопкаОтмена и/или КнопкаПринять, установим их.
            if (rootBlok != null)
            {
                // Установим для формы свойства.
                result = rootBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                for (int i1 = 0; i1 < result.Length; i1++)
                {
                    string strCurrent = result[i1];
                    if (strCurrent.Length >= 2)
                    {
                        if (strCurrent.Substring(0, 2) != @"//")
                        {
                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, formName + ".", "=");
                            if (displayName == "КнопкаОтмена" || displayName == "КнопкаПринять")
                            {
                                string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                PropValueConverter.SetPropValue(rootComponent, displayName, strPropertyValue);
                            }
	
                            if (displayName == "Меню")
                            {
                                string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                ((Form)rootComponent).Menu = (System.Windows.Forms.MainMenu)dictObjects[strPropertyValue];
                            }

                            if (strCurrent.StartsWith("Подсказка"))
                            {
                                displayName = "ToolTip на " + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".УстановитьПодсказку");
                                string strPropertyValue = strCurrent;
                                PropValueConverter.SetPropValue(rootComponent, displayName, strPropertyValue);
                            }
                        }
                    }
                }
                propertyGrid1.Refresh();
            }

            ComponentCollection ctrlsExisting = OneScriptFormsDesigner.DesignerHost.Container.Components;
            ISelectionService iSel = (ISelectionService)OneScriptFormsDesigner.DesignerHost.GetService(typeof(ISelectionService));
            if (iSel == null)
            {
                return;
            }
            iSel.SetSelectedComponents(new IComponent[] { ctrlsExisting[0] });

            OneScriptFormsDesigner.PropertyGridHost.ReloadTreeView();
            OneScriptFormsDesigner.PropertyGridHost.ChangeSelectNode((Component)ctrlsExisting[0]);

            OneScriptFormsDesigner.block2 = false;

            // Запомним начальное состояние дизайнера после загрузки этой формы.
            OneScriptFormsDesigner.DesignSurfaceState(true);
        }

        public string Version
        {
            get
            {
                if (string.IsNullOrEmpty(_version))
                {
                    // Получение версии файла запущенной сборки.
                    System.Diagnostics.FileVersionInfo FVI = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                    _version = FVI.ProductVersion;
                }
                return _version;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ChangeImage(bool change)
        {
            if (change)
            {
                _tabOrder1.CheckState = System.Windows.Forms.CheckState.Checked;
            }
            else
            {
                _tabOrder1.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
        }

        public Control GetmainForm()
        {
            return this;
        }

        private void pDesignerMainForm_Load(object sender, EventArgs e)
        {
            // Таймер для обеспечения срабатывания по правой кнопке мыши сворачивания раскрытого свойства СписокИзображений.
            timerLoad = new System.Windows.Forms.Timer();
            timerLoad.Enabled = true;
            timerLoad.Tick += new EventHandler(timerLoad_Tick);
        }

        private void _deleteForm_Click(object sender, EventArgs e)
        {
            if (pDesigner.TabControl.TabPages.Count <= 1)
            {
                MessageBox.Show(
                    "Удалить единственную форму не допускается.",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                    );
            }
            else
            {
                OneScriptFormsDesigner.block2 = true;
                OneScriptFormsDesigner.dictionaryTabPageChanged.Remove(pDesigner.TabControl.SelectedTab);
                IpDesignerCore.RemoveDesignSurface(IpDesignerCore.ActiveDesignSurface);
                OneScriptFormsDesigner.block2 = false;
            }
        }

        private void _unDo_Click(object sender, EventArgs e)
        {
            IpDesignerCore.UndoOnDesignSurface();
        }

        private void _reDo_Click(object sender, EventArgs e)
        {
            IpDesignerCore.RedoOnDesignSurface();
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            string cmd = (sender as ToolStripMenuItem).Text;
            if (cmd == "Вырезать")
            {
                IpDesignerCore.CutOnDesignSurface();
            }
            else if (cmd == "Копировать")
            {
                IpDesignerCore.CopyOnDesignSurface();
            }
            else if (cmd == "Вставить")
            {
                IpDesignerCore.PasteOnDesignSurface();
            }
            else if (cmd == "Удалить")
            {
                IpDesignerCore.DeleteOnDesignSurface();
            }
        }

        private void _tabOrder_Click(object sender, EventArgs e)
        {
            IpDesignerCore.SwitchTabOrder();

            if (_tabOrder.CheckState == System.Windows.Forms.CheckState.Unchecked)
            {
                _tabOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            }
            else if (_tabOrder.CheckState == System.Windows.Forms.CheckState.Checked)
            {
                _tabOrder.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
        }

        private void _about_Click(object sender, EventArgs e)
        {
            string str1 = "Дизайнер форм от ahyahy " + Environment.NewLine + 
                "Версия " + Version + Environment.NewLine + 
                "(Создана на основе программы: " + Environment.NewLine + 
                "picoFormDesigner coded by Paolo Foti " + Environment.NewLine +
                "Version is: 1.0.0.0)";
            MessageBox.Show(str1, "Дизайнер форм для OneScriptForms", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void _useSnapLines_Click(object sender, EventArgs e)
        {
            OneScriptFormsDesigner.block2 = true;
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.SnapLines, new Size(1, 1));
            OneScriptFormsDesigner.block2 = false;

            // Запомним начальное состояние дизайнера после добавления новой формы.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = true;
        }

        private void _useGrid_Click(object sender, EventArgs e)
        {
            OneScriptFormsDesigner.block2 = true;
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.Grid, new Size(16, 16));
            OneScriptFormsDesigner.block2 = false;

            // Запомним начальное состояние дизайнера после добавления новой формы.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = true;
        }

        private void _useGridWithoutSnapping_Click(object sender, EventArgs e)
        {
            OneScriptFormsDesigner.block2 = true;
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.GridWithoutSnapping, new Size(16, 16));
            OneScriptFormsDesigner.block2 = false;

            // Запомним начальное состояние дизайнера после добавления новой формы.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = true;
        }

        private void _useNoGuides_Click(object sender, EventArgs e)
        {
            OneScriptFormsDesigner.block2 = true;
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.NoGuides, new Size(1, 1));
            OneScriptFormsDesigner.block2 = false;

            // Запомним начальное состояние дизайнера после добавления новой формы.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = true;
        }

        private void pDesignerMainForm_Closing(object sender, CancelEventArgs e)
        {
            bool closeDesigner = true;
            foreach (KeyValuePair<System.Windows.Forms.TabPage, bool> keyValue in OneScriptFormsDesigner.dictionaryTabPageChanged)
            {
                if (keyValue.Value)
                {
                    closeDesigner = false;
                    break;
                }
            }

            if (!closeDesigner)
            {
                string str1 = "Одна из редактируемых форм изменена! Изменения будут потеряны!\n\nВыйти из конструктора форм?";
                if (MessageBox.Show(str1, "Дизайнер форм для OneScriptForms", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    e.Cancel = false;
                    return;
                }
            }
        }

        public static void SelectedNodeSearch(System.Windows.Forms.TreeView treeView, string nameNodeParent, ref TreeNode node, TreeNodeCollection treeNodes = null)
        {
            TreeNodeCollection _treeNodes;
            if (treeNodes == null)
            {
                _treeNodes = treeView.Nodes;
            }
            else
            {
                _treeNodes = treeNodes;
            }
            TreeNode treeNode = null;
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                treeNode = _treeNodes[i];
                if (((MenuItemEntry)treeNode.Tag).Name == nameNodeParent)
                {
                    node = treeNode;
                    break;
                }
                if (treeNode.Nodes.Count > 0)
                {
                    SelectedNodeSearch(treeView, nameNodeParent, ref node, treeNode.Nodes);
                }
            }
        }
    }

    public class PropertyGridMessageFilter : IMessageFilter
    {
        public Control Control; // Элемент управления для мониторинга.

        public MouseEventHandler MouseUp;

        public PropertyGridMessageFilter(Control c, MouseEventHandler meh)
        {
            Control = c;
            MouseUp = meh;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (!Control.IsDisposed && m.HWnd == Control.Handle && MouseUp != null)
            {
                System.Windows.Forms.MouseButtons mb = System.Windows.Forms.MouseButtons.None;

                switch (m.Msg)
                {
                    case 0x0202:/*WM_LBUTTONUP, see winuser.h*/
                        mb = System.Windows.Forms.MouseButtons.Left;
                        break;
                    case 0x0205:/*WM_RBUTTONUP*/
                        mb = System.Windows.Forms.MouseButtons.Right;
                        break;
                }

                if (mb != System.Windows.Forms.MouseButtons.None)
                {
                    MouseEventArgs e = new MouseEventArgs(mb, 1, m.LParam.ToInt32() & 0xFFff, m.LParam.ToInt32() >> 16, 0);
                    MouseUp(Control, e);
                }
            }
            return false;
        }
    }
}
