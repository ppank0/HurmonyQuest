using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContestService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Juries",
                columns: new[] { "Id", "Birthday", "CreatedAt", "Name", "Surname", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("02220676-0775-4f06-9c7b-cb169f443c95"), new DateOnly(1983, 3, 29), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Nat", "Kshlerin", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("38771bdd-42e9-4681-a811-02df65df89f0") },
                    { new Guid("96c89650-283b-46ee-8ce1-ddcd72c3fa9f"), new DateOnly(1953, 5, 2), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Phyllis", "Konopelski", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("13048571-68da-4bdc-9b8f-834f760b77a4") },
                    { new Guid("b0b2a94f-debe-4c15-8fb5-f550b658d611"), new DateOnly(2004, 10, 17), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Blanche", "Yost", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("61268129-382f-4108-ae82-09b09df6c89d") },
                    { new Guid("ea8bf84e-4361-4e4e-be6c-76a0a000895e"), new DateOnly(1977, 11, 14), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Hoyt", "Senger", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("962aafad-3036-42b1-9691-0ea673961986") },
                    { new Guid("f4e0af60-2da2-4862-8d48-01b32063ebc8"), new DateOnly(1948, 9, 12), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Tiana", "Stamm", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("4bfd943f-9408-4730-9b4e-8b3e136fa29e") }
                });

            migrationBuilder.InsertData(
                table: "Nominations",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("186bbe09-6947-4039-8c6a-0b1fbdd1f985"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "World", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("406f403f-43c8-431f-9351-6fe261036779"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Classical", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e2d8a1e7-898f-43cb-a9b8-ec19a97873ad"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Funk", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "CreatedAt", "EndDate", "Name", "StartDate", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("7af049ce-91cf-4472-9eab-3d2e1105b198"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 29, 1, 6, 25, 707, DateTimeKind.Utc).AddTicks(1947), "quis", new DateTime(2025, 4, 28, 1, 6, 25, 707, DateTimeKind.Utc).AddTicks(1947), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a5dab53b-d136-4317-95cc-1038b89dd18e"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 5, 6, 13, 34, 15, 668, DateTimeKind.Utc).AddTicks(6994), "distinctio", new DateTime(2025, 5, 4, 13, 34, 15, 668, DateTimeKind.Utc).AddTicks(6994), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c3a9758e-78fc-407f-a447-0e00f3485337"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 27, 1, 4, 38, 986, DateTimeKind.Utc).AddTicks(9177), "porro", new DateTime(2025, 4, 25, 1, 4, 38, 986, DateTimeKind.Utc).AddTicks(9177), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "MusicalInstruments",
                columns: new[] { "Id", "CreatedAt", "Name", "NominationId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0eed4b50-d4b9-4fc7-b2ca-6a54c0ec6c1a"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Refined Frozen Cheese", new Guid("186bbe09-6947-4039-8c6a-0b1fbdd1f985"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5d84636c-1b4f-4d8f-afdf-ae3373810503"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Handmade Metal Bacon", new Guid("186bbe09-6947-4039-8c6a-0b1fbdd1f985"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9acb28bf-4af7-4fc7-96d5-151dd3bb1e73"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Unbranded Steel Fish", new Guid("186bbe09-6947-4039-8c6a-0b1fbdd1f985"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9ddf2c9d-8b94-477c-9e51-df5cf9633736"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Refined Frozen Keyboard", new Guid("406f403f-43c8-431f-9351-6fe261036779"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c42aee05-858a-428c-a5d5-ea4a4ca8b9b4"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Fantastic Cotton Ball", new Guid("e2d8a1e7-898f-43cb-a9b8-ec19a97873ad"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cfb5d935-b0c1-47d3-b806-5fe0117beb9b"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Licensed Wooden Bacon", new Guid("186bbe09-6947-4039-8c6a-0b1fbdd1f985"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d407b303-63c2-4a0e-9ca5-fa853bb1ac2c"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Licensed Rubber Chair", new Guid("406f403f-43c8-431f-9351-6fe261036779"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d7cee254-b063-4be1-8611-0973c757f2f3"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Handmade Frozen Bike", new Guid("406f403f-43c8-431f-9351-6fe261036779"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dae78225-f9a6-4486-b2bb-2ff7fa3be40e"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Licensed Concrete Chips", new Guid("e2d8a1e7-898f-43cb-a9b8-ec19a97873ad"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dea1389c-8dcb-4da2-ad5f-4507173f7705"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Incredible Cotton Fish", new Guid("406f403f-43c8-431f-9351-6fe261036779"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("df7ef85b-7144-46f9-b0d9-5844f070ec78"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Generic Soft Shirt", new Guid("e2d8a1e7-898f-43cb-a9b8-ec19a97873ad"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f45683b8-a05a-45e8-bb0f-629f0b91aa5e"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Handmade Soft Computer", new Guid("e2d8a1e7-898f-43cb-a9b8-ec19a97873ad"), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "Id", "Birthday", "CreatedAt", "MusicalInstrumentId", "Name", "Surname", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("0df7992c-df4f-4bdb-bfbd-6f520d77c095"), new DateOnly(1948, 12, 5), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("cfb5d935-b0c1-47d3-b806-5fe0117beb9b"), "Nicklaus", "Okuneva", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("36dc9934-c664-4b18-b4be-31773974059d") },
                    { new Guid("25137d6c-c912-43d9-9495-b61f0777e1da"), new DateOnly(1949, 1, 12), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("dea1389c-8dcb-4da2-ad5f-4507173f7705"), "Mitchell", "Ledner", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("b3f654f5-d622-415f-801b-501c58a11cf9") },
                    { new Guid("2dde21b5-669e-4f10-b0f5-a0f7a737db44"), new DateOnly(1975, 6, 8), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("dea1389c-8dcb-4da2-ad5f-4507173f7705"), "Audie", "Armstrong", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("b50149cb-666d-4fc7-b864-2196a8e9b142") },
                    { new Guid("2fce6fae-2636-45e1-a689-78fb77036410"), new DateOnly(1996, 11, 3), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("5d84636c-1b4f-4d8f-afdf-ae3373810503"), "Jaren", "Howell", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("da16e1f3-a7e1-44e4-a8d9-eee430cb98b2") },
                    { new Guid("331d287f-18e0-402a-9824-60689a1a41d9"), new DateOnly(1956, 8, 22), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("cfb5d935-b0c1-47d3-b806-5fe0117beb9b"), "Davon", "Torphy", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("78fb34b2-e597-411e-9b8e-3dd4d00f4855") },
                    { new Guid("3414d950-900b-4983-bc3d-b9615420986b"), new DateOnly(1993, 7, 1), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("9acb28bf-4af7-4fc7-96d5-151dd3bb1e73"), "Aimee", "Volkman", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("0938fc27-766c-42ea-9d7b-3e60aeb13e07") },
                    { new Guid("3bcdb14d-446d-49c8-8153-91ce0139baaf"), new DateOnly(1953, 6, 12), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("dae78225-f9a6-4486-b2bb-2ff7fa3be40e"), "Alexandre", "Hackett", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("e07e68b4-9c15-4758-884a-93ee2ff7bac1") },
                    { new Guid("4257a716-ee8a-41a5-a610-d50d22d3f94c"), new DateOnly(1951, 4, 24), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("dea1389c-8dcb-4da2-ad5f-4507173f7705"), "Baron", "Stamm", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("619769e4-a332-4d48-a461-884487b66682") },
                    { new Guid("431be4ba-d3e1-4d8a-ba34-5cc2ec075a08"), new DateOnly(1964, 2, 27), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("9ddf2c9d-8b94-477c-9e51-df5cf9633736"), "Delmer", "Kilback", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("94d63026-e2a7-4581-a01b-d25d412c2610") },
                    { new Guid("4884b967-ff70-43b4-a186-628f1c052617"), new DateOnly(1982, 3, 18), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("d407b303-63c2-4a0e-9ca5-fa853bb1ac2c"), "Oral", "Stark", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("640ace8d-3f84-45e3-b73f-b8f0576372b1") },
                    { new Guid("491f9756-e426-4ee6-8704-b68992711ecc"), new DateOnly(1990, 1, 17), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("5d84636c-1b4f-4d8f-afdf-ae3373810503"), "Jordy", "Jakubowski", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("fee50472-e3ca-4c6e-b34f-54c9f40cb8fb") },
                    { new Guid("4d4caffe-587a-4d84-af6f-d2a007c22bbd"), new DateOnly(2005, 10, 1), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("f45683b8-a05a-45e8-bb0f-629f0b91aa5e"), "Tierra", "Stroman", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("84569f0a-1d3d-42e7-8100-e019222c660f") },
                    { new Guid("4eeef696-868d-4830-ac86-ff9c2d7de60b"), new DateOnly(1969, 2, 23), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("dae78225-f9a6-4486-b2bb-2ff7fa3be40e"), "Marianne", "Cassin", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("27449ef2-b2b2-4fd3-b349-db5e51d4e232") },
                    { new Guid("6293af47-5db6-4f5e-bc37-e9c7c8c94fa9"), new DateOnly(1991, 7, 11), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("9acb28bf-4af7-4fc7-96d5-151dd3bb1e73"), "Javon", "Schinner", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("9cdfd514-ed1e-48c2-bbc7-03a88b1aef46") },
                    { new Guid("632ccaf0-a404-4450-b04f-21293979e9cb"), new DateOnly(1987, 7, 20), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("d407b303-63c2-4a0e-9ca5-fa853bb1ac2c"), "Ellsworth", "Willms", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("a6f1cbf1-4cb0-42d3-b80f-0bdcd65807c7") },
                    { new Guid("6598db7b-e9be-4af8-a900-3ce6339244a5"), new DateOnly(1966, 2, 7), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("9ddf2c9d-8b94-477c-9e51-df5cf9633736"), "Nikko", "Gerlach", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("73fa0e6b-b8b7-4c47-a1c2-8f68b98b0608") },
                    { new Guid("695fe4ac-9217-4958-b829-82c2649616ff"), new DateOnly(1991, 4, 18), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("9ddf2c9d-8b94-477c-9e51-df5cf9633736"), "Wilford", "Bartell", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("386ede6f-14a5-4c94-9657-51d3fda7615b") },
                    { new Guid("76fb0351-325a-4d02-afc1-9eb119da8277"), new DateOnly(1956, 11, 2), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("5d84636c-1b4f-4d8f-afdf-ae3373810503"), "Emery", "Doyle", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("8d1f5001-bc15-4143-b58a-a2bd679a6928") },
                    { new Guid("783be7ab-20f5-4809-a53c-8fd913d1e699"), new DateOnly(1970, 4, 8), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("df7ef85b-7144-46f9-b0d9-5844f070ec78"), "Uriah", "Reinger", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("c28d102b-138f-42ee-820e-61306fa1cbc1") },
                    { new Guid("78da0424-a872-44cf-8ae7-bbe15010e2b5"), new DateOnly(1973, 2, 12), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("d7cee254-b063-4be1-8611-0973c757f2f3"), "Jamil", "Jacobs", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("301be1fb-0017-43b2-84af-6318960ebe41") },
                    { new Guid("812c296a-c2a6-4642-b7bf-4927e2f34632"), new DateOnly(1971, 3, 17), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("d7cee254-b063-4be1-8611-0973c757f2f3"), "Don", "Frami", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("690842af-f16b-4854-82c5-1865725b1362") },
                    { new Guid("8fd335ac-4df2-4fe2-8708-f0abbaf47781"), new DateOnly(1954, 5, 25), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("c42aee05-858a-428c-a5d5-ea4a4ca8b9b4"), "Walker", "Lehner", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("f236786b-9fd4-4641-bcaf-d07d66729433") },
                    { new Guid("94416735-6ae4-4786-a8cf-722de3a8972b"), new DateOnly(1969, 7, 16), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("cfb5d935-b0c1-47d3-b806-5fe0117beb9b"), "Nyasia", "Herzog", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("dacffa47-e02a-4198-b2fe-3ddef38fb30b") },
                    { new Guid("9528b056-3672-4a55-a701-c7b55b0bc4d4"), new DateOnly(1982, 8, 25), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("9acb28bf-4af7-4fc7-96d5-151dd3bb1e73"), "Bria", "Parker", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("e2c8cb3b-705f-4dc2-8a43-4960068f5af7") },
                    { new Guid("9ea38c43-df14-4cf0-a8d8-717a29fc96f2"), new DateOnly(1983, 8, 17), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("dae78225-f9a6-4486-b2bb-2ff7fa3be40e"), "Cristopher", "Lowe", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("851f6c79-4979-4c2e-87ca-c2daad3483d1") },
                    { new Guid("a0ab413c-17f6-468f-8fd4-053d8092a1c1"), new DateOnly(1996, 2, 27), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("cfb5d935-b0c1-47d3-b806-5fe0117beb9b"), "Harold", "Champlin", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("b35abd54-4eaf-4777-834e-daaa4f834e82") },
                    { new Guid("a7689877-2ce6-4331-91ee-e83ba6ddfcdd"), new DateOnly(1995, 3, 8), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("d407b303-63c2-4a0e-9ca5-fa853bb1ac2c"), "Jewel", "Larkin", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("5d6f8826-26ca-47fc-ba6d-aff1a82eb597") },
                    { new Guid("ada7f011-73ed-44f9-8746-a332dc463493"), new DateOnly(1986, 7, 30), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("df7ef85b-7144-46f9-b0d9-5844f070ec78"), "Dolores", "Mertz", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("da97baa7-ad9c-4122-9cd2-cf42db5b646d") },
                    { new Guid("ae6fdc6c-9635-429a-94ab-1e6a0c89e4a8"), new DateOnly(1971, 5, 1), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("0eed4b50-d4b9-4fc7-b2ca-6a54c0ec6c1a"), "Estevan", "Armstrong", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("3c5b792e-241f-47c1-ae19-ea2814659112") },
                    { new Guid("b28d0f13-8ece-475b-9bd5-df4fbd6de9f3"), new DateOnly(2001, 2, 15), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("c42aee05-858a-428c-a5d5-ea4a4ca8b9b4"), "Makenzie", "Ruecker", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("ff8b0170-0238-427b-af02-9654ac0ee7b9") },
                    { new Guid("b7dc85ba-91fb-4a6c-b4a4-f1050df428fd"), new DateOnly(1987, 3, 18), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("dae78225-f9a6-4486-b2bb-2ff7fa3be40e"), "Bartholome", "Douglas", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7dd769a6-75ad-4eb3-baa4-9d519398b4cd") },
                    { new Guid("bb419518-8924-43b7-9738-9f7d9d00d83d"), new DateOnly(1983, 12, 2), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("c42aee05-858a-428c-a5d5-ea4a4ca8b9b4"), "Caden", "Cassin", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("1cc9a40a-ee51-4823-85c4-ca65838a8876") },
                    { new Guid("bcd23079-ee8d-4fac-b8e1-3c27ed5b843b"), new DateOnly(2005, 12, 6), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("df7ef85b-7144-46f9-b0d9-5844f070ec78"), "Deshaun", "Corwin", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("a87e6b54-1bd1-4f81-a501-f64002893dc6") },
                    { new Guid("c2cc8d9c-4a9c-4dc4-b8da-f3d92bf17fa3"), new DateOnly(1957, 9, 9), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("d7cee254-b063-4be1-8611-0973c757f2f3"), "Domenico", "D'Amore", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("d8385332-dfd4-4795-84db-f92c2442977a") },
                    { new Guid("c3331c28-c953-4f20-acfe-e3d1cde53041"), new DateOnly(1959, 11, 6), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("5d84636c-1b4f-4d8f-afdf-ae3373810503"), "Gilbert", "Gleason", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("3b5542dc-456f-4544-ae68-53dc7ab0154c") },
                    { new Guid("d598dd06-f8e6-4712-b28e-60a582777cd3"), new DateOnly(1956, 10, 4), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("df7ef85b-7144-46f9-b0d9-5844f070ec78"), "Trey", "Veum", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("5c4c3387-d138-4410-8751-edb357bcc8d1") },
                    { new Guid("d68c716f-f3ab-492d-92fd-ae48a8bdea48"), new DateOnly(1959, 12, 30), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("9ddf2c9d-8b94-477c-9e51-df5cf9633736"), "Jeffry", "Prohaska", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("5c953f57-e8df-4e2d-9587-d5e19d8ad4fa") },
                    { new Guid("d7b14bfc-96a8-41af-b7ba-97e76d06ba3f"), new DateOnly(1999, 1, 6), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("f45683b8-a05a-45e8-bb0f-629f0b91aa5e"), "Layne", "Collier", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("b0e5ab6b-43e2-4d9b-981e-4c844f7c8f93") },
                    { new Guid("e2dc2921-b854-444a-b53d-6082168daf4c"), new DateOnly(1950, 9, 30), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("f45683b8-a05a-45e8-bb0f-629f0b91aa5e"), "Davion", "O'Conner", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("683f7337-eb08-41ee-be5b-ff4715ad4626") },
                    { new Guid("ec18dbed-ac57-4926-87de-562738c7f792"), new DateOnly(1981, 4, 26), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("f45683b8-a05a-45e8-bb0f-629f0b91aa5e"), "Shanny", "Mosciski", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("8b530981-93b5-4614-acb9-c3d553e06c13") },
                    { new Guid("edd309c0-2da3-4ecc-9c40-7d2e877aa1a5"), new DateOnly(1972, 3, 25), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("c42aee05-858a-428c-a5d5-ea4a4ca8b9b4"), "Kristy", "Stanton", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("eb62066a-ecb9-4717-9102-6a304b5a966e") },
                    { new Guid("f2333bdf-7e4d-400a-8420-2f16336880ef"), new DateOnly(1989, 12, 22), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("d407b303-63c2-4a0e-9ca5-fa853bb1ac2c"), "Claudine", "Waters", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("f33a0b60-2f88-4356-a545-911b58535f20") },
                    { new Guid("f29a1317-f44f-4b3e-b7a5-4c5d2f5a7b79"), new DateOnly(1948, 6, 13), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("dea1389c-8dcb-4da2-ad5f-4507173f7705"), "Akeem", "Lowe", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("d6d605e8-1e7c-424c-909c-d9a58bd9cb3b") },
                    { new Guid("f78c715b-4e34-4f8a-9b00-a9fa47b03bb3"), new DateOnly(1984, 1, 2), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("9acb28bf-4af7-4fc7-96d5-151dd3bb1e73"), "Jannie", "Carroll", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("ce55d2b4-7de7-4183-96e3-a9c27b4daefc") },
                    { new Guid("f7fcff80-7767-4488-a0fb-3bd3fbed2fba"), new DateOnly(1949, 5, 30), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("d7cee254-b063-4be1-8611-0973c757f2f3"), "Josefa", "O'Keefe", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("17a16040-56b2-4bf8-b269-75c4f54ac60c") },
                    { new Guid("fd03766e-4c99-4b4f-9bb6-1a0e6d10ee67"), new DateOnly(1996, 5, 8), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("0eed4b50-d4b9-4fc7-b2ca-6a54c0ec6c1a"), "Sunny", "Jones", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7350bbed-7547-4e68-adf7-cf44ebddddee") },
                    { new Guid("fea2754f-3a33-4400-a52f-03733fd1aba2"), new DateOnly(1978, 6, 6), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("0eed4b50-d4b9-4fc7-b2ca-6a54c0ec6c1a"), "Emanuel", "Casper", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("3bf9e07f-9e55-4d7e-a3c4-c00aa19834a0") },
                    { new Guid("ff040c21-1991-4e7b-ac1f-46d57c4c8e2b"), new DateOnly(1980, 12, 10), new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("0eed4b50-d4b9-4fc7-b2ca-6a54c0ec6c1a"), "Annamae", "Conroy", new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("c638533c-658e-4ee6-901f-681c969cb987") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Juries",
                keyColumn: "Id",
                keyValue: new Guid("02220676-0775-4f06-9c7b-cb169f443c95"));

            migrationBuilder.DeleteData(
                table: "Juries",
                keyColumn: "Id",
                keyValue: new Guid("96c89650-283b-46ee-8ce1-ddcd72c3fa9f"));

            migrationBuilder.DeleteData(
                table: "Juries",
                keyColumn: "Id",
                keyValue: new Guid("b0b2a94f-debe-4c15-8fb5-f550b658d611"));

            migrationBuilder.DeleteData(
                table: "Juries",
                keyColumn: "Id",
                keyValue: new Guid("ea8bf84e-4361-4e4e-be6c-76a0a000895e"));

            migrationBuilder.DeleteData(
                table: "Juries",
                keyColumn: "Id",
                keyValue: new Guid("f4e0af60-2da2-4862-8d48-01b32063ebc8"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("0df7992c-df4f-4bdb-bfbd-6f520d77c095"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("25137d6c-c912-43d9-9495-b61f0777e1da"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("2dde21b5-669e-4f10-b0f5-a0f7a737db44"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("2fce6fae-2636-45e1-a689-78fb77036410"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("331d287f-18e0-402a-9824-60689a1a41d9"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("3414d950-900b-4983-bc3d-b9615420986b"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("3bcdb14d-446d-49c8-8153-91ce0139baaf"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("4257a716-ee8a-41a5-a610-d50d22d3f94c"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("431be4ba-d3e1-4d8a-ba34-5cc2ec075a08"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("4884b967-ff70-43b4-a186-628f1c052617"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("491f9756-e426-4ee6-8704-b68992711ecc"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("4d4caffe-587a-4d84-af6f-d2a007c22bbd"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("4eeef696-868d-4830-ac86-ff9c2d7de60b"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("6293af47-5db6-4f5e-bc37-e9c7c8c94fa9"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("632ccaf0-a404-4450-b04f-21293979e9cb"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("6598db7b-e9be-4af8-a900-3ce6339244a5"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("695fe4ac-9217-4958-b829-82c2649616ff"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("76fb0351-325a-4d02-afc1-9eb119da8277"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("783be7ab-20f5-4809-a53c-8fd913d1e699"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("78da0424-a872-44cf-8ae7-bbe15010e2b5"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("812c296a-c2a6-4642-b7bf-4927e2f34632"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("8fd335ac-4df2-4fe2-8708-f0abbaf47781"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("94416735-6ae4-4786-a8cf-722de3a8972b"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("9528b056-3672-4a55-a701-c7b55b0bc4d4"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("9ea38c43-df14-4cf0-a8d8-717a29fc96f2"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("a0ab413c-17f6-468f-8fd4-053d8092a1c1"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("a7689877-2ce6-4331-91ee-e83ba6ddfcdd"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("ada7f011-73ed-44f9-8746-a332dc463493"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("ae6fdc6c-9635-429a-94ab-1e6a0c89e4a8"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("b28d0f13-8ece-475b-9bd5-df4fbd6de9f3"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("b7dc85ba-91fb-4a6c-b4a4-f1050df428fd"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("bb419518-8924-43b7-9738-9f7d9d00d83d"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("bcd23079-ee8d-4fac-b8e1-3c27ed5b843b"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("c2cc8d9c-4a9c-4dc4-b8da-f3d92bf17fa3"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("c3331c28-c953-4f20-acfe-e3d1cde53041"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("d598dd06-f8e6-4712-b28e-60a582777cd3"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("d68c716f-f3ab-492d-92fd-ae48a8bdea48"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("d7b14bfc-96a8-41af-b7ba-97e76d06ba3f"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("e2dc2921-b854-444a-b53d-6082168daf4c"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("ec18dbed-ac57-4926-87de-562738c7f792"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("edd309c0-2da3-4ecc-9c40-7d2e877aa1a5"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("f2333bdf-7e4d-400a-8420-2f16336880ef"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("f29a1317-f44f-4b3e-b7a5-4c5d2f5a7b79"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("f78c715b-4e34-4f8a-9b00-a9fa47b03bb3"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("f7fcff80-7767-4488-a0fb-3bd3fbed2fba"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("fd03766e-4c99-4b4f-9bb6-1a0e6d10ee67"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("fea2754f-3a33-4400-a52f-03733fd1aba2"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "Id",
                keyValue: new Guid("ff040c21-1991-4e7b-ac1f-46d57c4c8e2b"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("7af049ce-91cf-4472-9eab-3d2e1105b198"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("a5dab53b-d136-4317-95cc-1038b89dd18e"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("c3a9758e-78fc-407f-a447-0e00f3485337"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("0eed4b50-d4b9-4fc7-b2ca-6a54c0ec6c1a"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("5d84636c-1b4f-4d8f-afdf-ae3373810503"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("9acb28bf-4af7-4fc7-96d5-151dd3bb1e73"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("9ddf2c9d-8b94-477c-9e51-df5cf9633736"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("c42aee05-858a-428c-a5d5-ea4a4ca8b9b4"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("cfb5d935-b0c1-47d3-b806-5fe0117beb9b"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("d407b303-63c2-4a0e-9ca5-fa853bb1ac2c"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("d7cee254-b063-4be1-8611-0973c757f2f3"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("dae78225-f9a6-4486-b2bb-2ff7fa3be40e"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("dea1389c-8dcb-4da2-ad5f-4507173f7705"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("df7ef85b-7144-46f9-b0d9-5844f070ec78"));

            migrationBuilder.DeleteData(
                table: "MusicalInstruments",
                keyColumn: "Id",
                keyValue: new Guid("f45683b8-a05a-45e8-bb0f-629f0b91aa5e"));

            migrationBuilder.DeleteData(
                table: "Nominations",
                keyColumn: "Id",
                keyValue: new Guid("186bbe09-6947-4039-8c6a-0b1fbdd1f985"));

            migrationBuilder.DeleteData(
                table: "Nominations",
                keyColumn: "Id",
                keyValue: new Guid("406f403f-43c8-431f-9351-6fe261036779"));

            migrationBuilder.DeleteData(
                table: "Nominations",
                keyColumn: "Id",
                keyValue: new Guid("e2d8a1e7-898f-43cb-a9b8-ec19a97873ad"));
        }
    }
}
