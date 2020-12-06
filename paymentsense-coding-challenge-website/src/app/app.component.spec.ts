import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { PaymentsenseCodingChallengeApiService } from './services';
import { MockPaymentsenseCodingChallengeApiService } from './testing/mock-paymentsense-coding-challenge-api.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgxPaginationModule } from 'ngx-pagination';
import { By } from '@angular/platform-browser';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        FontAwesomeModule,
        NgxPaginationModule
      ],
      declarations: [
        AppComponent
      ],
      providers: [
        { provide: PaymentsenseCodingChallengeApiService, useClass: MockPaymentsenseCodingChallengeApiService }
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Paymentsense Coding Challenge'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('Paymentsense Coding Challenge!');
  });

  it('should render title in a h1 tag', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('h1').textContent).toContain('Paymentsense Coding Challenge!');
  });

  it('should render countries title in a h4 tag', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('h4').textContent).toContain('List of Countries');
  });
  

  it('should click a country to view details', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    let listElement =  fixture.debugElement.query(By.css('#lstCountries'));
    listElement.triggerEventHandler('click', null);
    
    fixture.whenStable().then(() => {
      const compiled = fixture.debugElement.nativeElement;
      expect(compiled.querySelector('p').textContent).toContain('Afghanistan');
    });
  });
  
});
