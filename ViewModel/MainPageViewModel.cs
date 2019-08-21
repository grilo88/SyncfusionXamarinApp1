using Syncfusion.SfDataGrid.XForms;
using SyncfusionXamarinApp1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace SyncfusionXamarinApp1.ViewModel
{

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly ContentPage page;
        public ObservableCollection<Pessoa> Pessoas { get; set; }

        #region Fields

        private string filterText = "";
        private string selectedColumn = "Todas as Colunas";
        private string selectedCondition = "Igual A";

        internal delegate void FilterChanged();
        internal FilterChanged filterTextChanged;

        #endregion

        #region Property

        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                OnFilterTextChanged();
                OnPropertyChanged();
            }
        }

        public string SelectedCondition
        {
            get { return selectedCondition; }
            set { selectedCondition = value; }
        }

        public string SelectedColumn
        {
            get { return selectedColumn; }
            set { selectedColumn = value; }
        }

        #endregion

        public MainPageViewModel(ContentPage page)
        {
            this.page = page;
            var dg = page.FindByName<SfDataGrid>("dataGrid");
            dg.FrozenColumnsCount = 1;

            Pessoas = new ObservableCollection<Pessoa>();
            Pessoas.Add(new Pessoa() { Nome = "Marcos", Sobrenome = "Andrade", Telefone = 62981485632, Atribuicao = "AN" });
            Pessoas.Add(new Pessoa() { Nome = "João", Sobrenome = "Vieira", Telefone = 1398574856, Atribuicao = "SM" });
            Pessoas.Add(new Pessoa() { Nome = "Rodrigo", Sobrenome = "Moreira", Telefone = 6185985465 });
            Pessoas.Add(new Pessoa() { Nome = "Ricado", Sobrenome = "Santos", Telefone = 2285985463 });
            Pessoas.Add(new Pessoa() { Nome = "André", Sobrenome = "Souze", Telefone = 2358569854 });
            Pessoas.Add(new Pessoa() { Nome = "Renato", Sobrenome = "Andrade", Telefone = 62981485632 });
            Pessoas.Add(new Pessoa() { Nome = "Vinícius", Sobrenome = "Vieira", Telefone = 1398574856, Atribuicao = "SM" });
            Pessoas.Add(new Pessoa() { Nome = "João", Sobrenome = "Moreira", Telefone = 6185985465 });
            Pessoas.Add(new Pessoa() { Nome = "Lucas", Sobrenome = "Santos", Telefone = 2285985463 });
            Pessoas.Add(new Pessoa() { Nome = "Mateus", Sobrenome = "Santana", Telefone = 2358569854 });
            Pessoas.Add(new Pessoa() { Nome = "Joel", Sobrenome = "Freitas", Telefone = 62981485632, Atribuicao = "AN" });
            Pessoas.Add(new Pessoa() { Nome = "Judas", Sobrenome = "Vieira", Telefone = 1398574856, Atribuicao = "SM" });
            Pessoas.Add(new Pessoa() { Nome = "Guilherme", Sobrenome = "Moreira de Alencar", Telefone = 6185985465 });
            Pessoas.Add(new Pessoa() { Nome = "Frederico", Sobrenome = "Moreira dos Santos Marques", Telefone = 2285985463 });
            Pessoas.Add(new Pessoa() { Nome = "Rafael", Sobrenome = "Souze", Telefone = 2358569854 });
            Pessoas.Add(new Pessoa() { Nome = "Guilherme Caíque", Sobrenome = "Andrade", Telefone = 62981485632 });
            Pessoas.Add(new Pessoa() { Nome = "Maike", Sobrenome = "Apolinário", Telefone = 1398574856 });
            Pessoas.Add(new Pessoa() { Nome = "Diego", Sobrenome = "Costa", Telefone = 6185985465 });
            Pessoas.Add(new Pessoa() { Nome = "Diogo", Sobrenome = "Perim", Telefone = 2285985463 });
            Pessoas.Add(new Pessoa() { Nome = "Douglas", Sobrenome = "Perim", Telefone = 2358569854 });
        }

        #region Private Methods

        private void OnFilterTextChanged()
        {
            filterTextChanged();
        }

        private bool MakeStringFilter(Pessoa o, string option, string condition)
        {
            var value = o.GetType().GetProperty(option);
            var exactValue = value.GetValue(o, null);
            exactValue = exactValue.ToString().ToLower();
            string text = FilterText.ToLower();
            var methods = typeof(string).GetMethods();
            if (methods.Count() != 0)
            {
                if (condition == "Contém")
                {
                    var methodInfo = methods.FirstOrDefault(method => method.Name == condition);
                    bool result1 = (bool)methodInfo.Invoke(exactValue, new object[] { text });
                    return result1;
                }
                else if (exactValue.ToString() == text.ToString())
                {
                    bool result1 = String.Equals(exactValue.ToString(), text.ToString());
                    if (condition == "Igual A")
                        return result1;
                    else if (condition == "Diferente De")
                        return false;
                }
                else if (condition == "Diferente De")
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private bool MakeNumericFilter(Pessoa o, string option, string condition)
        {
            var value = o.GetType().GetProperty(option);
            var exactValue = value.GetValue(o, null);
            double res;
            bool checkNumeric = double.TryParse(exactValue.ToString(), out res);
            if (checkNumeric)
            {
                switch (condition)
                {
                    case "Igual A":
                        try
                        {
                            if (exactValue.ToString() == FilterText)
                            {
                                if (Convert.ToDouble(exactValue) == (Convert.ToDouble(FilterText)))
                                    return true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case "Diferente De":
                        try
                        {
                            if (Convert.ToDouble(FilterText) != Convert.ToDouble(exactValue))
                                return true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        #endregion

        #region Public Methods

        public bool FilerRecords(object o)
        {
            double res;
            bool checkNumeric = double.TryParse(FilterText, out res);
            var item = o as Pessoa;
            if (item != null && FilterText.Equals(""))
            {
                return true;
            }
            else
            {
                if (item != null)
                {
                    if (checkNumeric && !SelectedColumn.Equals("Todas as Colunas"))
                    {
                        bool result = MakeNumericFilter(item, SelectedColumn, SelectedCondition);
                        return result;
                    }
                    else if (SelectedColumn.Equals("Todas as Colunas"))
                    {
                        if (item.Nome.ToLower().Contains(FilterText.ToLower()) ||
                            item.Sobrenome.ToLower().Contains(FilterText.ToLower()) ||
                            item.Telefone.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.Atribuicao.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.DataCadastro.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.Nascimento.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.Ativo.ToString().ToLower().Contains(FilterText.ToLower()))
                            return true;
                        return false;
                    }
                    else
                    {
                        bool result = MakeStringFilter(item, SelectedColumn, SelectedCondition);
                        return result;
                    }
                }
            }
            return false;
        }

        #endregion

        public void OnPropertyChanged([CallerMemberName]string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
