import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    standalone: true,
    imports: [CommonModule, FormsModule, HttpClientModule]
})
export class HomeComponent implements OnInit {
    entityTypes: string[] = [];

    constructor(private http: HttpClient, private router: Router) {}

    ngOnInit(): void {
        this.loadEntityTypes();
    }

    loadEntityTypes(): void {
        this.http.get<string[]>('https://localhost:7111/api/entities')
            .subscribe(
                data => this.entityTypes = data,
                error => console.error('Помилка при завантаженні типів сутностей', error)
            );
    }

    createEntity(): void {
        const typeSelect = document.getElementById("entityType") as HTMLSelectElement;

        if (!typeSelect) {
            console.error("Не знайдено елемент вибору сутності");
            return;
        }

        const type = typeSelect.value;

        if (!type || type === "Виберіть сутність") {
            alert("Будь ласка, оберіть тип сутності для створення");
            return;
        }

        this.router.navigate(['/create'], { queryParams: { type } });
    }

    editEntity(): void {
        const typeSelect = document.getElementById("entityType") as HTMLSelectElement;
        const idInput = document.getElementById("entityId") as HTMLInputElement;

        if (!typeSelect || !idInput) {
            console.error("Не знайдено необхідні елементи форми");
            return;
        }

        const type = typeSelect.value;
        const id = idInput.value;

        if (!type || type === "Виберіть сутність") {
            alert("Будь ласка, оберіть сутність");
            return;
        }

        if (!id) {
            alert("Будь ласка, введіть ID");
            return;
        }

        this.router.navigate(['/update'], { queryParams: { type, id } });
    }

    deleteEntity(): void {
      const typeSelect = document.getElementById("entityType") as HTMLSelectElement;
      const idInput = document.getElementById("entityId") as HTMLInputElement;

      if (!typeSelect || !idInput) {
      console.error("Не знайдено необхідні елементи форми");
      return;
      }

      const type = typeSelect.value;
      const id = idInput.value;

      if (!type || type === "Виберіть сутність") {
      alert("Будь ласка, оберіть сутність");
      return;
      }

      if (!id) {
      alert("Будь ласка, введіть ID");
      return;
      }

      this.http.delete(`https://localhost:7111/api/Delete?type=${type}&id=${id}`)
      .subscribe(
        () => alert('Сутність успішно видалена'),
        error => console.error('Помилка при видаленні сутності', error)
      );
    }
}
