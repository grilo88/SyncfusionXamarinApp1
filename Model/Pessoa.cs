using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SyncfusionXamarinApp1.Model
{
    public class Pessoa : INotifyPropertyChanged, IEditableObject
    {
        public static int quant = 0;

        private string nome = "Nome";
        private string sobrenome = "Sobrenome";
        private long telefone = 62985734296;
        private bool ativo = true;
        private string atribuicao = "PU";
        private DateTime nascimento = DateTime.Now;
        private DateTime dataCadastro = DateTime.Now;

        public string Nome { get => nome; set { nome = value; OnPropertyChanged(); } }
        public string Sobrenome { get => sobrenome; set { sobrenome = value; OnPropertyChanged(); } }
        public long Telefone { get => telefone; set { telefone = value; OnPropertyChanged(); } }
        public int Horas { get => horas; set { horas = value; OnPropertyChanged(); } }
        public bool Ativo { get => ativo; set { ativo = value; OnPropertyChanged(); } }
        public string Atribuicao { get => atribuicao; set { atribuicao = value; OnPropertyChanged(); } }
        public DateTime Nascimento { get => nascimento; set { nascimento = value; OnPropertyChanged(); } }
        public DateTime DataCadastro { get => dataCadastro; set { dataCadastro = value; OnPropertyChanged(); } }

        public Pessoa()
        {
            Random rnd = new Random(Environment.TickCount + ++Pessoa.quant);
            horas = rnd.Next(0, 100);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            // TODO: Armazenar qual propriedade sofreu alteração para ser usada por EndEdit()

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private Dictionary<string, object> storedValues;
        private int horas = 0;

        public void BeginEdit()
        {
            this.storedValues = this.BackUp();
        }

        public void CancelEdit()
        {
            if (this.storedValues == null)
                return;

            foreach (var item in this.storedValues)
            {
                var itemProperties = this.GetType().GetTypeInfo().DeclaredProperties;
                var pDesc = itemProperties.FirstOrDefault(p => p.Name == item.Key);
                if (pDesc != null)
                    pDesc.SetValue(this, item.Value);
            }
        }

        public void EndEdit()
        {
            // TODO: Salvar novo valor no banco de dados

            if (this.storedValues != null)
            {
                this.storedValues.Clear();
                this.storedValues = null;
            }
            Debug.WriteLine("End Edit Called");
        }

        protected Dictionary<string, object> BackUp()
        {
            var dictionary = new Dictionary<string, object>();
            var itemProperties = this.GetType().GetTypeInfo().DeclaredProperties;
            foreach (var pDescriptor in itemProperties)
            {
                if (pDescriptor.CanWrite)
                    dictionary.Add(pDescriptor.Name, pDescriptor.GetValue(this));
            }
            return dictionary;
        }
    }
}