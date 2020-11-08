interface ToDo {
  id: string;
  text: string;
  priority: number;
  tags: string[];
  deadline: string;
  assignedTo: Assigner;
}
