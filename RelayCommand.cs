using System.Windows.Input;
namespace CustomDialog;
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;        // 리턴값이 없는 Delegate Class 임.
    private readonly Predicate<object> _canExecute;  // 리턴값이 있는 Delegate Class 임.

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {

        // execute 가 null이 아니면 execute 를 _execute 에 할당하고
        // execute 가 null이면 ArgumentNullException 을 throw 한다.
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter)
    {
        // _canExecute 가 null 이면 True 리턴 하고,
        // _canExecute 가 null 이 아니면 _canExecute 를 실행하고 결과를 true 또는 false 로 리턴한다.
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        // 명령을 실행 한다.
        _execute(parameter);
    }
}
