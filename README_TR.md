# Fatura Arşivleme Sistemi

Bu proje, **Fatura Arşivleme Sistemi** olarak tasarlanmış olup, faturaların işlenmesini, saklanmasını ve yönetimini sağlayan modern bir backend mimarisine sahiptir.

## 📜 Proje Genel Bakış

Sistem şu işlemleri gerçekleştirebilir:
- **Faturaları kaydetme** (MongoDB üzerinde doküman tabanlı veri saklama).
- **Fatura listesini getirme** ve **fatura detaylarını görüntüleme**.
- **Faturaları MongoDB'den MSSQL'e taşıma** (Hangfire ile zamanlanmış iş kullanarak).
- **RabbitMQ mesaj kuyruğu ile faturaları "saklandı" olarak işaretleme**.
- **Başarıyla saklanan faturalar için e-posta bildirimi gönderme**.
- **Elasticsearch ve Kibana ile sistem aktivitelerini kaydetme**.

## 🏗️ Sistem Mimarisi

![Sistem Diyagramı](./InvoiceArchive-Diagram.png)

### 🛠️ Kullanılan Teknolojiler

- **.NET Core 8** (Web API)
- **MongoDB** (Doküman tabanlı veritabanı)
- **MSSQL** (İlişkisel veritabanı)
- **RabbitMQ** (Mesaj kuyruğu)
- **Hangfire** (Arkaplan iş zamanlayıcı)
- **MassTransit** (Mesajlaşma katmanı)
- **Elasticsearch & Kibana** (Log yönetimi ve izleme)
- **Serilog** (Yapılandırılmış loglama)
- **MediatR & CQRS** (Sorgu ve komut ayrımı prensibi)
- **Repository Pattern** (Veritabanı soyutlama katmanı)
- **FluentValidation** (İstek doğrulama)
- **AutoMapper** (Nesne eşleme)

## 🚀 API Uç Noktaları

### 1️⃣ **Fatura Yönetimi**
```http
POST /createinvoice
```
**Açıklama:** Yeni bir fatura kaydeder.

```http
GET /getinvoices
```
**Açıklama:** Fatura başlık listesini getirir.

```http
GET /getinvoicebyid/{id}
```
**Açıklama:** ID’ye göre fatura getirir.

### 2️⃣ **Saklanan Fatura Yönetimi**
```http
GET /getstoredinvoices
```
**Açıklama:** MSSQL'deki saklanan faturaların listesini getirir.

```http
GET /getstoredinvoicebyid/{id}
```
**Açıklama:** Veritabanı ID’sine göre faturayı getirir.

```http
GET /getstoredinvoicebyinvoiceid/{invoiceid}
```
**Açıklama:** Fatura kimliğine göre saklanan faturayı getirir.

```http
GET /getstoredinvoiceslist
```
**Açıklama:** Saklanan faturaların listesini getirir.

### 3️⃣ **Faturaları Saklamak İçin Zamanlanmış İş**
```http
GET /addorupdatejobstoreinvoices?cronExpression=*/15 * * * *
```
**Açıklama:** MongoDB'deki faturaları MSSQL'e taşıyan zamanlanmış bir iş ekler veya günceller.

## 🏗️ Sistem Akışı

1. **Fatura Kaydı**  
   - API yeni bir fatura talebi alır.
   - Fatura **MongoDB'ye kaydedilir**.
   - Elasticsearch istekleri loglar.

2. **Arkaplan İşlemi (Hangfire)**  
   - **İşlenmemiş faturaları alır**.
   - **Bunları MSSQL’e kaydeder**.
   - **RabbitMQ mesajı** göndererek MongoDB'deki faturayı `IsStored = true` olarak günceller.

3. **Saklama Sonrası İşlemler**  
   - **RabbitMQ üzerinden başarı olayı** yayınlanır.
   - **SMTP ile e-posta bildirimi** gönderilir.

## 🔥 Loglama & İzleme

- **Serilog** ile API istekleri kaydedilir.
- **Elasticsearch** logları saklar.
- **Kibana** logları görselleştirmek için kullanılır.

## 🏁 Sonuç

Bu proje, **CQRS, mesajlaşma, zamanlanmış işlemler ve loglama** entegrasyonu içeren ölçeklenebilir bir fatura arşivleme çözümüdür.

## 📜 Case Gereksinimleri (Orijinal)

**Senaryo**  
1. **API tasarımı** şu metodları içermelidir:
   - **Yeni fatura yükleme**.
   - **Fatura başlıklarını listeleme**.
   - **Belirli bir faturayı ID ile getirme**.

2. Fatura **veritabanına kaydedilmelidir**.

3. **Zamanlanmış bir iş** işlenmemiş faturaları alıp saklamalıdır.

4. **E-posta bildirimleri** gönderilmelidir.

5. **README dokümantasyonu** çalıştırma talimatlarını içermelidir.

**Backend:** .NET Core (.NET 5 veya .NET 6)  
**Opsiyonel:** Loglama (Serilog, ELK, vb.)
