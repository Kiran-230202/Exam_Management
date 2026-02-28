import { Routes } from '@angular/router';
import { ExamFormComponent } from './pages/exam-form/exam-form.component';
import { ExamListComponent } from './pages/exam-list/exam-list.component';

export const routes: Routes = [
    { path: '', redirectTo: 'exam-list', pathMatch: 'full' },
    { path: 'exam-form', component: ExamFormComponent },
    { path: 'exam-list', component: ExamListComponent }
];
