import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
    selector: 'app-update',
    templateUrl: './update.component.html',
    styleUrls: ['./update.component.css'],
    standalone: true,
    imports: [FormsModule, CommonModule, HttpClientModule]
})
export class UpdateComponent implements OnInit {
    entityType: string = '';
    entityId: number = 0;
    properties: { key: string, value: any, label: string }[] = [];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private http: HttpClient
    ) { }

    ngOnInit(): void {
        this.entityType = this.route.snapshot.queryParamMap.get('type') || '';
        this.entityId = +this.route.snapshot.queryParamMap.get('id')!;
        this.loadEntity();
    }

    loadEntity(): void {
        this.http.get<{ entityType: string, id: number, properties: { [key: string]: any } }>(`https://localhost:7111/api/Update/Edit?type=${this.entityType}&id=${this.entityId}`)
            .subscribe(
                data => {
                    this.properties = Object.keys(data.properties).map(key => ({
                        key,
                        value: data.properties[key],
                        label: this.generateLabel(key)
                    }));
                },
                error => console.error('Помилка при завантаженні даних сутності', error)
            );
    }

    generateLabel(field: string): string {
        // Convert field name to a more readable label
        return field.replace(/([A-Z])/g, ' $1').replace(/^./, str => str.toUpperCase());
    }

    /*onSubmit(): void {
      const updatedValues = this.properties.reduce((acc: { [key: string]: any }, prop) => {
          if (prop.value !== null && prop.value !== '') { // Exclude empty values
              acc[prop.key] = prop.value; // Add key-value pairs to the object
          }
          return acc;
      }, {});
  
      console.log('Updated values:', updatedValues);
  
      this.http.post(`https://localhost:7111/api/Update/UpdateEntity?type=${this.entityType}&id=${this.entityId}`, updatedValues)
          .subscribe(
              response => {
                  alert('Запис успішно оновлено');
                  this.router.navigate(['/home']);
              },
              error => {
                  console.error('Помилка при оновленні запису', error);
                  if (error.error && error.error.errors) {
                      console.error('Деталі помилки:', error.error.errors);
                  }
                  alert('Помилка при оновленні запису');
              }
          );
  }*/
          onSubmit(): void {
            const updatedValues: { [key: string]: any } = {};
            for (const prop of this.properties) {
                // Перетворюємо значення на рядки
                updatedValues[prop.key] = String(prop.value); // Збираємо всі властивості як рядки
            }
        
            this.http.post(`https://localhost:7111/api/Update/UpdateEntity?type=${this.entityType}&id=${this.entityId}`, updatedValues)
                .subscribe(
                    response => {
                        alert('Запис успішно оновлено');
                        this.router.navigate(['/home']);
                    },
                    error => {
                        console.error('Помилка при оновленні запису', error);
                        alert('Помилка при оновленні запису: ' + (error.error?.message || error.message));
                    }
                );
        }
}