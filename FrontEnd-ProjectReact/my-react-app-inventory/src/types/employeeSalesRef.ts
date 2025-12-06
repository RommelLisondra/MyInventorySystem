import type { Employee } from "./employee";

export interface EmployeeSalesRef {
    id: number;
    empIdno: string;
  
    employeeNavigation:  Employee;
}
