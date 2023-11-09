import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import { CreateContactComponent } from '../create-contact/create-contact.component';
import { MatDialog } from '@angular/material/dialog';
import { UpdateContactComponent } from '../update-contact/update-contact.component';
@Component({
  selector: 'app-contact',
  templateUrl: './contactList.component.html',
  styleUrls: ['./contactList.component.css']
})
export class ContactListComponent implements OnInit {

  headers = ['Name', 'Position', 'Office', 'Age', 'Start Date', 'Salary'];
  public contactList: Contact[] = [];
  public baseUrl: any;
  public getContactSubscription:any;
 
  constructor(private http: HttpClient, private dialog: MatDialog) {
    this.baseUrl = environment.apiUrl;
  }
  
  getContactList(){
    this.getContactSubscription = this.http.get(`${this.baseUrl}/api/getAllContacts`).subscribe((result: any) => {
      this.contactList = result;
    });
  }

  openCreateContact(): void{
    var dialogRef = this.dialog.open(CreateContactComponent,
      {
      panelClass: 'pop-up-new-tab',
      hasBackdrop:false,
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result !== "cancel"){
        this.getContactList();
        alert("Successfully Contact Created!");
      }
    });
  }

  openUpdateContact(id:any): void{
    var dialogRef = this.dialog.open((UpdateContactComponent),
      {
      panelClass: 'pop-up-new-tab',
      hasBackdrop:false,
      data:{key:id}
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
        this.getContactList();
    });
  }

  ngOnInit(): void {
    this.getContactList();
  }

  deleteContact(id:any):void{
    this.getContactSubscription = this.http.delete(`${this.baseUrl}/api/deleteContact?contactId=${id}`).subscribe((result: any) => {
      if(result.isSuccess){
        alert("Successfully Contact Deleted!");
        this.getContactList();
      }
    });
  }

  ngOnDestroy():void{
    this.getContactSubscription?.unsubscribe();
  }
}

interface Contact{
 id:number;
 firstName: string;
 lastName:string;
 email: string;
 phoneNumber: string;
 address: string;
 city: string;
 state: string;
 country: string;
 postalCode: string;
 isSuccess: boolean;
}