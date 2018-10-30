using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Materal.WPFCustomControlLib.SearchBox
{
    public class SearchBox : ComboBox
    {


        /// <summary>
        /// 查询方法
        /// </summary>
        public Func<object, bool> SearchFun { get; set; } = m => true;

        /// <summary>
        /// 候选数据
        /// </summary>
        public IEnumerable<object> CandidateData { get => (IEnumerable<object>)GetValue(CandidateDataProperty); set => SetValue(CandidateDataProperty, value); }
        public static readonly DependencyProperty CandidateDataProperty = DependencyProperty.Register(nameof(CandidateData), typeof(IEnumerable<object>), typeof(SearchBox),
            new FrameworkPropertyMetadata(
                new List<object>(),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnCandidateDataChanged,
                CoerceCandidateData));
        private static void OnCandidateDataChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is SearchBox searchBox)) return;
            searchBox.ItemsSource = searchBox.CandidateShowData;
        }
        private static object CoerceCandidateData(DependencyObject sender, object value)
        {
            if (!(sender is SearchBox searchBox)) return value;
            if (!(value is IEnumerable<object> values)) return value;
            IEnumerable<object> result = values.ToList().Where(searchBox.SearchFun);
            searchBox.CandidateShowData.Clear();
            foreach (object item in result)
            {
                searchBox.CandidateShowData.Add(item);
            }
            return value;
        }
        /// <summary>
        /// 候选项显示数据
        /// </summary>
        public ObservableCollection<object> CandidateShowData { get; set; } = new ObservableCollection<object>();
        /// <summary>
        /// 应用模版
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("PART_EditableTextBox") is TextBox textBox) textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateShowData();
            IsDropDownOpen = CandidateShowData.Count > 0;
            if (sender is TextBox textBox)
            {
                textBox.SelectionStart = Text.Length;
            }
        }
        /// <summary>
        /// 更新显示数据
        /// </summary>
        public void UpdateShowData()
        {
            object[] data = CandidateData.Where(SearchFun).ToArray();
            CandidateShowData.Clear();
            foreach (object item in data)
            {
                CandidateShowData.Add(item);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        static SearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox), new FrameworkPropertyMetadata(typeof(SearchBox)));
        }
    }
}
