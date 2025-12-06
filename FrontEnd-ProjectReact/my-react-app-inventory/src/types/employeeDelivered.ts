import type { Employee } from "./employee";

export interface EmployeeDelivered {
    id: number;
    empIdno: string;
  
    employeeNavigation:  Employee;
}
