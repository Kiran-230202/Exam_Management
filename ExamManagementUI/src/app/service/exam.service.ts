import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ExamMaster, Student, Subject } from '../model/exam.models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'  
})
export class ExamService {
  private baseUrl =  environment.apiBaseUrl; 
  constructor(private http: HttpClient) { }

  getAllStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(`${this.baseUrl}/GetAllStudents`).pipe(
      map((res: Student[]) =>{
        return res;
      })
    )
  }

  getAllSubjects(): Observable<Subject[]>{
    return this.http.get<Subject[]>(`${this.baseUrl}/GetAllSubjects`).pipe(
      map((res: Subject[]) =>{
        return res;
      })
    )
  }

  saveExam(exam: ExamMaster): Observable<ExamMaster> {
    return this.http.post<ExamMaster>(`${this.baseUrl}/SaveExam`, exam).pipe(
      map((res: ExamMaster) =>{
        return res;
      })
    )
  }


  getSavedExams(): Observable<ExamMaster[]> {
  return this.http.get<ExamMaster[]>(`${this.baseUrl}/GetSavedExams`).pipe(
    map((res: ExamMaster[]) => {
      return res;
    })
  );
  }
}