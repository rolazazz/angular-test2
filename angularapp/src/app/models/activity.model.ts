export interface Activity {
  project: Project;
  employee: Employee;
  date: Date;
  hours: number;
}


export interface Project {
  id: number;
  name: string;
}


export interface Employee {
  id: number;
  name: string;
}
