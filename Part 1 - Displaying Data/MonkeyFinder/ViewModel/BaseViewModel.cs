using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyFinder.ViewModel;

//[INotifyPropertyChanged]
public partial class BaseViewModel : ObservableObject
{

    public BaseViewModel()
    {

    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    [ObservableProperty]
    private string _title;

    public bool IsNotBusy => !IsBusy;

}

//public class BaseViewModel : INotifyPropertyChanged
//{
//    private bool _isBusy = false;
//    private string _title;

//    public bool IsBusy
//    {
//        get => _isBusy;
//        set
//        {
//            if (_isBusy == value)
//            {
//                return;
//            }

//            _isBusy = value;
//            OnPropertyChanged(nameof(IsBusy));
//            OnPropertyChanged(nameof(IsNotBusy));
//        }
//    }

//    public string Title
//    {
//        get => _title;
//        set
//        {
//            if (_title == value)
//            {
//                return;
//            }

//            _title = value;
//            OnPropertyChanged(nameof(Title));
//        }
//    }

//    public bool IsNotBusy => !IsBusy;

//    public event PropertyChangedEventHandler PropertyChanged;

//    public void OnPropertyChanged(string name)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
//    }
//}
