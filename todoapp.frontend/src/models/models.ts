export interface TodoList {
    id?: string,
    title: string,
    description?: string,
    hidden: boolean,
    createdDate?: string,
}

export interface Todo {
    id?: string,
    title: string,
    completed: boolean,
    dueDate?: string,
}