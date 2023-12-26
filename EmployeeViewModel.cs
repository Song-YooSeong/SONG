using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CustomDialog;

public class EmployeeViewModel : INotifyPropertyChanged
{
    private ICommand _inquiryCommand;  // 조회버튼
    private ICommand _updateCommand;  // 변경버튼
    private ICommand _clearCommand;  // Clear 버튼
    private string _empNo; // 조회버튼 누를때 입력했던 사번저장 변수
    private ObservableCollection<string> _logs = new(); // 실행로그
    private ObservableCollection<Employee> _employee; // 사원목록
    private Employee _selectedEmployee; // 데이터그리드에서 선택된 사원
    private string _selectedLog; // Logs 에서 선택된 데이터 한건

    EmployeeDao Dao = new EmployeeDao();


    public ObservableCollection<Employee> Employees
    {
        get { return _employee; }

        set
        {
            if (_employee != value)
            {
                _employee = value;
                OnPropertyChanged(nameof(Employees));

            }
        }
    }

    public ObservableCollection<string> Logs
    {
        get { return _logs; }

        set
        {
            if (_logs != value)
            {
                _logs = value;
                OnPropertyChanged(nameof(Logs));
            }
        }
    }


    public Employee SelectedEmployee
    {

        get { return _selectedEmployee; }

        set
        {
            if (_selectedEmployee != value)
            {

                
                // SelectedEmployee 의 Property 가 변경되는것을 감지 하기 위한 EventHandler 를 등록 한다.
                if (_selectedEmployee != null)
                {
                    _selectedEmployee.PropertyChanged -= SelectedEmployee_PropertyChanged;
                    ;
                }

                _selectedEmployee = value;

                if (_selectedEmployee != null)
                {
                    _selectedEmployee.PropertyChanged += SelectedEmployee_PropertyChanged;
                    Logs.Add($"Selected Item ->{_selectedEmployee.EmpNo}");
                }
                
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }





    }

    public string SelectedLog
    {
        get { return _selectedLog; }

        set
        {
            if (_selectedLog != value)
            {
                _selectedLog = value;
                // 상세로그를 화면상에 팝업으로 출력함. 2023.12.21
                /**
                DialogViewModel model = new DialogViewModel(value.ToString());
                CustomDialogWindow customDialogWindow = new CustomDialogWindow(model);
                customDialogWindow.Show();
                */

            }
        }
    }

    public string EmpNo
    {
        get { return _empNo; }

        set
        {
            if (_empNo != value)
            {
                _empNo = value;
                OnPropertyChanged(nameof(EmpNo));
                Logs.Add($"Input EmpNo ->{value}");
            }
        }
    }


    public ICommand InquiryCommand
    {
        get
        {
            if (_inquiryCommand == null)
            {
                _inquiryCommand = new RelayCommand(InquiryEmployee, CanInquiryEmployee);
            }
            return _inquiryCommand;
        }
    }

    private void InquiryEmployee(object parameter)
    {
        try
        {
            Employees = Dao.GetData(EmpNo);
            foreach (Employee employee in Employees)
            {
                employee.IsChanged = false;
            }
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
            Logs.Add(e.StackTrace);
        }
    }

    private bool CanInquiryEmployee(object parameter)
    {
        // 여기에 명령을 실행할지 말지 판단함.
        //return !string.IsNullOrWhiteSpace(EmpNo);
        return true;
    }


    public ICommand UpdateCommand
    {
        get
        {
            if (_updateCommand == null)
            {
                _updateCommand = new RelayCommand(UpdateEmployee, CanUpdateEmployee);
            }
            return _updateCommand;
        }
    }

    private void UpdateEmployee(object parameter)
    {
        try
        {
            int count = Dao.UpdateData(Employees);
            Employees = Dao.GetData(string.Empty);
            Logs.Add($"Update Employee ->{count} 건");
            foreach (Employee employee in Employees)
            {
                employee.IsChanged = false;
            }
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
            Logs.Add(e.StackTrace);
        }
    }

    private bool CanUpdateEmployee(object parameter)
    {
        // 만일, Employees 객체의 내용이 한건이라도 isChanged=true 이면, true 리턴 그렇지 않으면 false 리턴.
        // 즉, 사용자가 한건이라도 수정 헀다면 수정한 데이터를 업데이트 할 수 있게 버튼이 활성화 되고, 그렇지 않으면 버튼이 비활성화 된다.
        if (_employee != null)
        {
            foreach (Employee employee in Employees)
            {
                if (employee.IsChanged) return true;
            }
        }

        return false;
    }


    public ICommand ClearCommand
    {
        get
        {
            if (_clearCommand == null)
            {
                _clearCommand = new RelayCommand(ClearLog, CanClearLog);
            }
            return _clearCommand;
        }
    }

    private void ClearLog(object parameter)
    {
        // 실행로그 초기화
        Logs.Clear();
    }
    private bool CanClearLog(object parameter)
    {
        // 여기에 명령을 실행할지 말지 판단함.
        if (Logs.Count == 0) return false;
        else return true;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    // Property 가 변경되면 UI(xaml)에 변경되었다는 이벤트를 알려줌.
    // UI(xaml)은 변경되었음을 통보 받으면자동으로 바인딩 된 데이터로 화면을 갱신함.
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    // SelectedEmployee 의 Property 가 변경 되면, 실행로그에 그 값을 보여주기 위한 로직 추가 됨. - 2023.12.22
    protected virtual void SelectedEmployee_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        {
            // Employee 객체의 속성이 변경될 때 필요한 로직을 수행
            // 예: Ename 속성이 변경되었을 때의 처리
            if (e.PropertyName == nameof(SelectedEmployee.Ename))
            {
                // 처리 로직 작성
                // 이 부분에서 Ename이 변경된 것을 감지할 수 있습니다.
                Logs.Add($"Changed Property -> {nameof(SelectedEmployee.Ename)},{SelectedEmployee.Ename}");
            }

            if (e.PropertyName == nameof(SelectedEmployee.Sal))
            {
                // 처리 로직 작성
                // 이 부분에서 Ename이 변경된 것을 감지할 수 있습니다.
                Logs.Add($"Changed Property -> {nameof(SelectedEmployee.Sal)},{SelectedEmployee.Sal}");
            }

            if (e.PropertyName == nameof(SelectedEmployee.Job))
            {
                // 처리 로직 작성
                // 이 부분에서 Ename이 변경된 것을 감지할 수 있습니다.
                Logs.Add($"Changed Property -> {nameof(SelectedEmployee.Job)},{SelectedEmployee.Job}");
            }

            if (e.PropertyName == nameof(SelectedEmployee.Mgr))
            {
                // 처리 로직 작성
                // 이 부분에서 Ename이 변경된 것을 감지할 수 있습니다.
                Logs.Add($"Changed Property -> {nameof(SelectedEmployee.Mgr)},{SelectedEmployee.Mgr}");
            }

            if (e.PropertyName == nameof(SelectedEmployee.HireDate))
            {
                // 처리 로직 작성
                // 이 부분에서 Ename이 변경된 것을 감지할 수 있습니다.
                Logs.Add($"Changed Property -> {nameof(SelectedEmployee.HireDate)},{SelectedEmployee.HireDate}");
            }

            if (e.PropertyName == nameof(SelectedEmployee.Comm))
            {
                // 처리 로직 작성
                // 이 부분에서 Ename이 변경된 것을 감지할 수 있습니다.
                Logs.Add($"Changed Property -> {nameof(SelectedEmployee.Comm)},{SelectedEmployee.Comm}");
            }

            if (e.PropertyName == nameof(SelectedEmployee.DeptNo))
            {
                // 처리 로직 작성
                // 이 부분에서 Ename이 변경된 것을 감지할 수 있습니다.
                Logs.Add($"Changed Property -> {nameof(SelectedEmployee.DeptNo)},{SelectedEmployee.DeptNo}");
            }
        }
    }
}
