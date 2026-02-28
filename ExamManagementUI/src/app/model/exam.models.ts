export class Student {
    studentID?: number;        
    studentName: string = '';
    mail: string = '';

    examMasters?: ExamMaster[];
}

export class Subject {
    subjectID?: number;       
    subjectName: string = '';

    examDtls?: ExamDtls[];
}

export class ExamDtls {
    dtlsID?: number;           
    masterID: number = 0;
    subjectID: number = 0;
    marks: number = 0;

    examMaster?: ExamMaster;
    subjectEntity?: Subject;
}

export class ExamMaster {
    masterID?: number;         
    studentID: number = 0;
    studentName?: string;  
    examYear: number = 0;
    createTime?: Date;          
    totalMark?: number;         
    passOrFail?: string;        

    student?: Student;
    examDtls?: ExamDtls[];
}