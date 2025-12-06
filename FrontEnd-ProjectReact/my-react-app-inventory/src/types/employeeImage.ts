import type { Employee } from "./employee";

export interface EmployeeImage {
  id: number;
  empIdno: string;
  FilePath?: string;

  employeeNavigation:  Employee;

}
