import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule

@Component({
    selector: 'app-create',
    templateUrl: './create.component.html',
    styleUrls: ['./create.component.css'],
    standalone: true,
    imports: [FormsModule, CommonModule, HttpClientModule] // Add HttpClientModule here
})
export class CreateComponent implements OnInit {
    entityType: string = ''; // Initialize entityType
    properties: { key: string, value: any, label: string }[] = [];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private http: HttpClient
    ) { }

    ngOnInit(): void {
        this.entityType = this.route.snapshot.queryParamMap.get('type') || '';
        this.loadEntityFields();
    }

    loadEntityFields(): void {
        this.http.get<string[]>(`https://localhost:7111/api/entities/${this.entityType}`)
            .subscribe(
                data => {
                    this.properties = data.map(field => ({
                        key: field,
                        value: '',
                        label: this.generateLabel(field)
                    }));
                },
                error => console.error('Помилка при завантаженні полів сутності', error)
            );
    }

    generateLabel(field: string): string {
        // Convert field name to a more readable label
        return field.replace(/([A-Z])/g, ' $1').replace(/^./, str => str.toUpperCase());
    }

    onSubmit(): void {
        const values: { [key: string]: any } = this.properties.reduce((acc: { [key: string]: any }, prop) => {
            if (prop.value) { // Include only filled fields
                acc[prop.key] = prop.value;
            }
            return acc;
        }, {});

        console.log('Values to create:', values);

        this.http.post(`https://localhost:7111/api/Create?type=${this.entityType}`, values)
            .subscribe(
                response => {
                    console.log('Response from server:', response);
                    alert('Сутність успішно створена');
                    this.router.navigate(['/home']);
                },
                error => {
                    console.error('Помилка при створенні сутності', error);
                    alert('Помилка при створенні сутності');
                }
            );
    }
}
