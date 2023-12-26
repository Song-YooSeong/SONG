using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace CustomDialog;

class EmployeeDao
{

    // 입력된 txtEmpno 의 정보를 DB 에서 select 하는 메소드
    public ObservableCollection<Employee> GetData(string? txtEmpno)
    {
        ObservableCollection<Employee> RtnEmployee = new ObservableCollection<Employee>();
        try
        {
            OracleConnection conn = GetConnection();

            string sql = "select EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO from EMP";
            if (!string.IsNullOrEmpty(txtEmpno))
            {
                sql += $" where EMPNO={txtEmpno}";
            }

            OracleCommand cmd = new(sql, conn);
            conn.Open();

            OracleDataReader dr = cmd.ExecuteReader();

            // 이곳에서 데이터를 읽어와서 Employee 객체 목록을 생성한다.
            while (dr.Read())
            {
                Employee employee = new();

                employee.EmpNo = "" + dr["EMPNO"];
                employee.Ename = (string)dr["ENAME"];
                employee.Job = (string)dr["JOB"];
                employee.Mgr = "" + dr["MGR"];

                object hiredate = dr["HIREDATE"];
                if (!Convert.IsDBNull(hiredate))
                    employee.HireDate = ((DateTime)hiredate).ToString("yyyy-MM-dd");

                object sal = dr["SAL"];
                employee.Sal = FormatNumberWithCommas((int)(float)sal);

                object comm = dr["COMM"];
                employee.Comm = FormatNumberWithCommas((int)(float)comm);

                employee.DeptNo = "" + dr["DEPTNO"];

                RtnEmployee.Add(employee);
            }

            dr.Close();

            // DB Conneciton 을 Close 한다.
            CloseConnection(conn);
        }
        catch (Exception e)
        {
            throw;
        }
        return RtnEmployee;
    }

    // 사원정보를 DB에 업데이트 함.
    public int UpdateData(ObservableCollection<Employee> Employees)
    {
        int UpdateCnt = 0;
        try
        {
            int cnt;
            foreach (Employee Emp in Employees)
            {
                //만일, IsChanged 값이 true 이면 Update 문장을 생성하여 DB 데이터를 업데이트 한다.
                //즉, Employee 컬렉션객체들 중에 변경된 데이터만 DB 데이터를 업데이트 한다.

                if (!Emp.IsChanged) continue; // IsChanged 가 true 가 아니면, 즉 IsChanged 가 false 이면
                UpdateData(Emp);
                UpdateCnt++;

            }

        }
        catch (Exception e)
        {
            throw;
        }
        return UpdateCnt;
    }

    // 사원정보를 DB에 업데이트 함.
    private void UpdateData(Employee Emp)
    {
        try
        {
            StringBuilder sqlBuffer = new();
            OracleConnection conn = GetConnection();
            conn.Open();
            sqlBuffer.Append("update EMP set ");
            sqlBuffer.Append($"ENAME='{Emp.Ename}',");
            sqlBuffer.Append($"JOB='{Emp.Job}',");
            sqlBuffer.Append($"MGR={Emp.Mgr},");
            sqlBuffer.Append($"HIREDATE = TO_DATE('{Emp.HireDate}','YYYY-MM-DD'),");

            Emp.Sal = Emp.Sal.Replace(",", "");
            if (string.IsNullOrEmpty(Emp.Sal)) sqlBuffer.Append($"SAL=0,");
            else sqlBuffer.Append($"SAL={Emp.Sal},");


            Emp.Comm = Emp.Comm.Replace(",", "");
            if (string.IsNullOrEmpty(Emp.Comm)) sqlBuffer.Append($"COMM=0,");
            else sqlBuffer.Append($"COMM={Emp.Comm},");

            Emp.DeptNo = Emp.DeptNo.Replace(",", "");
            if (string.IsNullOrEmpty(Emp.DeptNo)) sqlBuffer.Append($"DEPTNO=0,");
            else sqlBuffer.Append($"DEPTNO={Emp.DeptNo} ");

            sqlBuffer.Append($" WHERE EMPNO={Emp.EmpNo} ");

            OracleCommand cmd = new(sqlBuffer.ToString(), conn);

            // SQL 문장을 ListBox 에 추가 한다.
            //lstSql.Items.Add(sqlBuffer.ToString());

            // SQL 문장을 실행한다.
            cmd.ExecuteNonQuery();

            // SQL 문장을 새롭게 생성하기 위해 스트링버퍼를 clear 한다.
            sqlBuffer.Clear();
            CloseConnection(conn);

        }
        catch (Exception e)
        {
            throw;
        }
    }


    // 오라클 DBMS 와 커넥션을 생성하고 그 결과객체(=OracleConnection) 를 반환한다.
    public OracleConnection GetConnection()
    {
        string remoteOracleServer = "10.1.60.29";
        string portNumber = "5521"; // Oracle의 기본 포트 번호는 1521이나, 필요에 따라 수정
        string serviceName = "kwdev";
        string userId = "scott";
        string password = "tiger";

        OracleConnection? conn;

        try
        {
            string connectionString = $"User Id={userId};Password={password};Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={remoteOracleServer})(PORT={portNumber})))(CONNECT_DATA=(SERVICE_NAME={serviceName})))";
            conn = new OracleConnection(connectionString);
        }
        catch (Exception e)
        {
           throw;
        }

        return conn;
    }

    // OracleConnection 을 Close 한다.(DB에 Commit 된다)
    public void CloseConnection(OracleConnection conn)
    {
        try
        {
            conn.Close();
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public string FormatNumberWithCommas(int number)
    {
        // 숫자를 컴마가 있는 형태로 변환
        string formattedNumber = string.Format("{0:#,##0}", number);
        return formattedNumber;
    }
}
