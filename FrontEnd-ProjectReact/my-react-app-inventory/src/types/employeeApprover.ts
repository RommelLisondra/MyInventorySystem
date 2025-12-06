import type { Employee } from "./employee";

export interface EmployeeApprover {
  id: number;
  empIdno: string;

  employeeNavigation:  Employee;
}
