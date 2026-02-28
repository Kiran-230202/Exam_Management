import { Component } from '@angular/core';
import { ExamMaster } from '../../model/exam.models';
import { ExamService } from '../../service/exam.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoaderComponent } from '../../components/loader/loader.component';

@Component({
  selector: 'app-exam-list',
  imports: [CommonModule, RouterModule, LoaderComponent],
  templateUrl: './exam-list.component.html',
  styleUrl: './exam-list.component.scss'
})
export class ExamListComponent {
  exams: ExamMaster[] = [];
  isLoading = false; 
  constructor(private examService: ExamService) { }

  ngOnInit(): void {
    this.loadExams();
  }

  loadExams() {
    this.isLoading = true;
    this.examService.getSavedExams().subscribe({
      next: (res) => {
        this.exams = res;
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isLoading = false;
      }
    });
  }

}
