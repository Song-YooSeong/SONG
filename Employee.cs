using System.ComponentModel;

namespace CustomDialog;

public class Employee : INotifyPropertyChanged
{
    private string empNo;
    public string EmpNo
    {
        get { return empNo; }
        set
        {
            if (empNo != value)
            {
                empNo = value;

            }
        }
    }

    private string eName;

    public string? Ename
    {
        get { return eName; }
        set
        {
            if (eName != value)
            {
                eName = value;

                //값이 변경되었음을 참조하고 있는 객체들에게 알려주기 위해서 OnPropertyChanged 메소드를 호출함.                
                OnPropertyChanged(nameof(Ename));

                //객체의 특정 Property 값이 변경되었으니, 이 객체의 값들을 DB에 변경 시킬수 있도록 isChanged를 true 로 설정.
                //이 값은 비즈니스로직상 필요에 의해서 변경 하는 것임.
                isChanged = true; 

            }
        }
    }

    private string job;
    public string? Job
    {
        get { return job; }
        set
        {
            if (job != value)
            {
                job = value;
                //값이 변경되었음을 참조하고 있는 객체들에게 알려주기 위해서 OnPropertyChanged 메소드를 호출함.
                OnPropertyChanged(nameof(Job));
                //객체의 특정 Property 값이 변경되었으니, 이 객체의 값들을 DB에 변경 시킬수 있도록 isChanged를 true 로 설정.
                //이 값은 비즈니스로직상 필요에 의해서 변경 하는 것임.
                isChanged = true;

            }
        }
    }

    private string mgr;
    public string? Mgr
    {
        get { return mgr; }
        set
        {
            if (mgr != value)
            {
                mgr = value;
                //값이 변경되었음을 참조하고 있는 객체들에게 알려주기 위해서 OnPropertyChanged 메소드를 호출함.
                OnPropertyChanged(nameof(Mgr));
                //객체의 특정 Property 값이 변경되었으니, 이 객체의 값들을 DB에 변경 시킬수 있도록 isChanged를 true 로 설정.
                //이 값은 비즈니스로직상 필요에 의해서 변경 하는 것임.
                isChanged = true;
            }
        }
    }

    private string hireDate;
    public string? HireDate
    {
        get { return hireDate; }
        set
        {
            if (hireDate != value)
            {
                hireDate = value;
                //값이 변경되었음을 참조하고 있는 객체들에게 알려주기 위해서 OnPropertyChanged 메소드를 호출함.
                OnPropertyChanged(nameof(HireDate));
                //객체의 특정 Property 값이 변경되었으니, 이 객체의 값들을 DB에 변경 시킬수 있도록 isChanged를 true 로 설정.
                //이 값은 비즈니스로직상 필요에 의해서 변경 하는 것임.
                isChanged = true;
            }
        }
    }
    private string sal;
    public string? Sal
    {
        get { return sal; }
        set
        {
            if (sal != value)
            {
                sal = value;
                //값이 변경되었음을 참조하고 있는 객체들에게 알려주기 위해서 OnPropertyChanged 메소드를 호출함.
                OnPropertyChanged(nameof(Sal));
                //객체의 특정 Property 값이 변경되었으니, 이 객체의 값들을 DB에 변경 시킬수 있도록 isChanged를 true 로 설정.
                //이 값은 비즈니스로직상 필요에 의해서 변경 하는 것임.
                isChanged = true;
            }
        }
    }
    private string comm;
    public string? Comm
    {
        get { return comm; }
        set
        {
            if (comm != value)
            {
                comm = value;
                //값이 변경되었음을 참조하고 있는 객체들에게 알려주기 위해서 OnPropertyChanged 메소드를 호출함.
                OnPropertyChanged(nameof(Comm));
                //객체의 특정 Property 값이 변경되었으니, 이 객체의 값들을 DB에 변경 시킬수 있도록 isChanged를 true 로 설정.
                //이 값은 비즈니스로직상 필요에 의해서 변경 하는 것임.
                isChanged = true;
            }
        }
    }
    private string deptNo;
    public string? DeptNo
    {
        get { return deptNo; }
        set
        {
            if (deptNo != value)
            {
                deptNo = value;
                //값이 변경되었음을 참조하고 있는 객체들에게 알려주기 위해서 OnPropertyChanged 메소드를 호출함.
                OnPropertyChanged(nameof(DeptNo));
                //객체의 특정 Property 값이 변경되었으니, 이 객체의 값들을 DB에 변경 시킬수 있도록 isChanged를 true 로 설정.
                //이 값은 비즈니스로직상 필요에 의해서 변경 하는 것임.
                isChanged = true;
            }
        }
    }

    private bool isChanged;
    public bool IsChanged
    {
        get { return isChanged; }
        set
        {
            if (isChanged != value)
            {
                isChanged = value;
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}