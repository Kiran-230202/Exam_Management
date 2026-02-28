import { Component } from '@angular/core';
import { ExamDtls, ExamMaster, Student, Subject } from '../../model/exam.models';
import { ExamService } from '../../service/exam.service';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { map, Observable, startWith } from 'rxjs';
import { SnackbarService } from '../../service/snackbar.service';
import { LoaderComponent } from '../../components/loader/loader.component';

@Component({
  selector: 'app-exam-form',
  imports: [FormsModule, CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatAutocompleteModule, LoaderComponent],
  templateUrl: './exam-form.component.html',
  styleUrl: './exam-form.component.scss'
})
export class ExamFormComponent {
  students: Student[] = [];
  subjects: Subject[] = [];

  examMaster: ExamMaster = new ExamMaster();
  examDetails: ExamDtls[] = [];

  selectedSubjectID: number = 0;
  marks: number = 0;

  totalMark: number = 0;

  studentControl = new FormControl('');
  filteredStudents!: Observable<Student[]>;
  subjectControl = new FormControl('');
  filteredSubjects!: Observable<Subject[]>;
  isLoading = false;

  constructor(private examService: ExamService, private router: Router, private snackbar: SnackbarService) { }

  ngOnInit(): void {
    this.examService.getAllStudents().subscribe(res => {
      this.students = res
      this.filteredStudents = this.studentControl.valueChanges.pipe(
        startWith(''),
        map(value => {
          this.autoDetectStudent(value);
          return this._filterStudents(value || '')
        })
      );
    });

    this.examService.getAllSubjects().subscribe(res => {
      this.subjects = res;

      this.filteredSubjects = this.subjectControl.valueChanges.pipe(
        startWith(''),
        map(value => {
          this.autoDetectSubject(value);
          return this._filterSubjects(value);
        })
      );
    });

  }


  private autoDetectStudent(value: any) {

    if (typeof value !== 'string') {
      this.examMaster.studentID = value?.studentID || 0;
      return;
    }

    const typedValue = value.toLowerCase().trim();

    const matchedStudent = this.students.find(
      s => s.studentName.toLowerCase() === typedValue
    );

    if (matchedStudent) {
      this.examMaster.studentID = matchedStudent.studentID!;
    } else {
      this.examMaster.studentID = 0;
    }
  }

  private autoDetectSubject(value: any) {

    if (typeof value !== 'string') {
      this.selectedSubjectID = value?.subjectID || 0;
      return;
    }

    const typedValue = value.toLowerCase().trim();

    const matchedSubject = this.subjects.find(
      s => s.subjectName.toLowerCase() === typedValue
    );

    if (matchedSubject) {
      this.selectedSubjectID = matchedSubject.subjectID!;
    } else {
      this.selectedSubjectID = 0;
    }
  }

  private _filterStudents(value: any): Student[] {
    const filterValue = (typeof value === 'string' ? value : value?.studentName)?.toLowerCase() || '';

    return this.students.filter(student =>
      student.studentName.toLowerCase().includes(filterValue)
    );
  }

  private _filterSubjects(value: any): Subject[] {

    const filterValue =
      typeof value === 'string'
        ? value.toLowerCase()
        : value?.subjectName?.toLowerCase() || '';

    return this.subjects.filter(subject =>
      subject.subjectName.toLowerCase().includes(filterValue)
    );
  }

  onStudentSelected(student: Student) {
    this.examMaster.studentID = student.studentID!;
  }

  displayStudent(student: Student): string {
    return student ? student.studentName : '';
  }

  displaySubject(subject: Subject): string {
    return subject ? subject.subjectName : '';
  }

  getSubjectName(subjectID: number | string): string {
    if (!this.subjects || this.subjects.length === 0) return '';

    const idStr = subjectID.toString();

    const subject = this.subjects.find(s => s.subjectID?.toString() === idStr);
    if (!subject) {
      return '';
    }
    return subject.subjectName;
  }

  addSubject() {

    if (!this.selectedSubjectID || this.marks < 0 || this.marks > 100) {
      this.snackbar.error("Invalid subject or marks");
      return;
    }
    const alreadyExists = this.examDetails.some(
      x => x.subjectID === this.selectedSubjectID
    );

    if (alreadyExists) {
      this.snackbar.error("This subject is already added.");
      return;
    }
    const detail = new ExamDtls();
    detail.subjectID = this.selectedSubjectID;
    detail.marks = this.marks;

    this.examDetails.push(detail);

    this.calculateTotal();

    this.subjectControl.setValue('');
    this.selectedSubjectID = 0;
    this.marks = 0;
  }

  calculateTotal() {
    this.totalMark = this.examDetails.reduce((sum, x) => sum + x.marks, 0);
  }

  saveExam() {
    const controlValue = this.studentControl.value;
    let matchedStudent: Student | undefined;
    // Case 1: If selected from dropdown (object)
    if (typeof controlValue === 'object' && controlValue !== null) {
      matchedStudent = controlValue;
    }

    // Case 2: If manually typed (string)
    else if (typeof controlValue === 'string') {
      matchedStudent = this.students.find(
        s => s.studentName.toLowerCase() === controlValue.toLowerCase()
      );
    }

    this.examMaster.examDtls = this.examDetails;
    this.examMaster.totalMark = this.examDetails.reduce((sum, x) => sum + x.marks, 0);

    this.examMaster.passOrFail = this.examDetails.every(x => x.marks >= 25) ? 'PASS' : 'FAIL';

    this.examService.saveExam(this.examMaster).subscribe({
      next: () => {
        this.isLoading = false;
        this.snackbar.success("Exam saved successfully!");
        this.resetForm();
        this.router.navigate(['/exam-list']);
      },
      error: (err) => {
        this.isLoading = false;

        let errorMessage = "Failed to save exam.";
        if (err?.error?.errors) {
          const validationErrors = err.error.errors;
          errorMessage = Object.values(validationErrors)
            .flat()
            .join('\n');
        }
        else if (err?.error?.message) {
          errorMessage = err.error.message;
        }
        console.log(errorMessage);
        this.snackbar.error(errorMessage);
      }
    });
  }

  resetForm() {
    this.examMaster = new ExamMaster();
    this.examDetails = [];
    this.totalMark = 0;
  }
  cancel() {
    this.router.navigate(['/exam-list']);
  }

  validateMarks(value: number, index: number) {
    if (value < 0) {
      this.examDetails[index].marks = 0;
    } else if (value > 100) {
      this.examDetails[index].marks = 100;
    }
    this.calculateTotal();
  }

  // Remove subject from the list
  removeSubject(index: number) {
    this.examDetails.splice(index, 1);
    this.calculateTotal();
  }
}
