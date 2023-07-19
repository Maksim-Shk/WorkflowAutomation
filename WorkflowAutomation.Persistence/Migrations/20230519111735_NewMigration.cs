using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WorkflowAutomation.Persistence.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.CreateTable(
                name: "AllowedSubdivisions",
                columns: table => new
                {
                    IdSubdivision = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IdSubordination = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AspNetDeviceCode",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Data = table.Column<string>(type: "character varying(50000)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetDeviceCode", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "document_type",
                columns: table => new
                {
                    id_document_type = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    short_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("document_type_pkey", x => x.id_document_type);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Use = table.Column<string>(type: "text", nullable: true),
                    Algorithm = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "boolean", nullable: false),
                    DataProtected = table.Column<bool>(type: "boolean", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Data = table.Column<string>(type: "character varying(50000)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "position",
                columns: table => new
                {
                    id_position = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    id_subordination = table.Column<int>(type: "integer", nullable: true),
                    short_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("position_pkey", x => x.id_position);
                    table.ForeignKey(
                        name: "position_id_subordination_fkey",
                        column: x => x.id_subordination,
                        principalTable: "position",
                        principalColumn: "id_position");
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id_status = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("status_pkey", x => x.id_status);
                });

            migrationBuilder.CreateTable(
                name: "subdivision",
                columns: table => new
                {
                    id_subdivision = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    id_subordination = table.Column<int>(type: "integer", nullable: true),
                    short_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("subdivision_pkey", x => x.id_subdivision);
                    table.ForeignKey(
                        name: "subdivision_id_subordination_fkey",
                        column: x => x.id_subordination,
                        principalTable: "subdivision",
                        principalColumn: "id_subdivision");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_user",
                columns: table => new
                {
                    id_user = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    surname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    last_online = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    register_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    removal_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("app_user_pkey", x => x.id_user);
                    table.ForeignKey(
                        name: "app_user_asp_id_fkey",
                        column: x => x.id_user,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    id_document = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_document_type = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    remove_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    update_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_sender = table.Column<string>(type: "text", nullable: false),
                    id_receiver = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("document_pkey", x => x.id_document);
                    table.ForeignKey(
                        name: "document_id_document_type_fkey",
                        column: x => x.id_document_type,
                        principalTable: "document_type",
                        principalColumn: "id_document_type");
                    table.ForeignKey(
                        name: "document_id_receiver_fkey",
                        column: x => x.id_receiver,
                        principalTable: "app_user",
                        principalColumn: "id_user");
                    table.ForeignKey(
                        name: "document_id_sender_fkey",
                        column: x => x.id_sender,
                        principalTable: "app_user",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "user_position",
                columns: table => new
                {
                    id_user_position = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_position = table.Column<int>(type: "integer", nullable: false),
                    appointment_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    removal_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_position_pkey", x => x.id_user_position);
                    table.ForeignKey(
                        name: "user_position_id_position_fkey",
                        column: x => x.id_position,
                        principalTable: "position",
                        principalColumn: "id_position");
                    table.ForeignKey(
                        name: "user_position_id_user_fkey",
                        column: x => x.id_user,
                        principalTable: "app_user",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "user_subdivision",
                columns: table => new
                {
                    id_user_subdivision = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_subdivision = table.Column<int>(type: "integer", nullable: false),
                    appointment_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    removal_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_subdivision_pkey", x => x.id_user_subdivision);
                    table.ForeignKey(
                        name: "user_subdivision_id_subdivision_fkey",
                        column: x => x.id_subdivision,
                        principalTable: "subdivision",
                        principalColumn: "id_subdivision");
                    table.ForeignKey(
                        name: "user_subdivision_id_user_fkey",
                        column: x => x.id_user,
                        principalTable: "app_user",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "document_content",
                columns: table => new
                {
                    id_document_content = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_document = table.Column<int>(type: "integer", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("document_content_pkey", x => x.id_document_content);
                    table.ForeignKey(
                        name: "document_content_id_document_fkey",
                        column: x => x.id_document,
                        principalTable: "document",
                        principalColumn: "id_document");
                });

            migrationBuilder.CreateTable(
                name: "document_status",
                columns: table => new
                {
                    id_document_status = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_document = table.Column<int>(type: "integer", nullable: false),
                    id_status = table.Column<int>(type: "integer", nullable: false),
                    appropriation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("document_status_pkey", x => x.id_document_status);
                    table.ForeignKey(
                        name: "document_status_id_document_fkey",
                        column: x => x.id_document,
                        principalTable: "document",
                        principalColumn: "id_document");
                    table.ForeignKey(
                        name: "document_status_id_status_fkey",
                        column: x => x.id_status,
                        principalTable: "status",
                        principalColumn: "id_status");
                    table.ForeignKey(
                        name: "document_status_id_user_fkey",
                        column: x => x.id_user,
                        principalTable: "app_user",
                        principalColumn: "id_user");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35e4b4a7-f767-43a9-9fd6-9aca58617027", null, "Администратор", "АДМИНИСТРАТОР" },
                    { "53669294-44e0-4f22-9516-b3ff5146a70f", null, "Заблокированный пользователь", "ЗАБЛОКИРОВАННЫЙ ПОЛЬЗОВАТЕЛЬ" },
                    { "a1bd31a5-01dc-48e9-b65c-5070bd5b0cee", null, "Зарегистрированный пользователь", "ЗАРЕГИСТРИРОВАННЫЙ ПОЛЬЗОВАТЕЛЬ" }
                });

            migrationBuilder.InsertData(
                table: "document_type",
                columns: new[] { "id_document_type", "name", "short_name" },
                values: new object[,]
                {
                    { 1, "Служебная записка", "СЗ" },
                    { 2, "Приказ", "Приказ" }
                });

            migrationBuilder.InsertData(
                table: "position",
                columns: new[] { "id_position", "id_subordination", "name", "short_name" },
                values: new object[,]
                {
                    { 1, null, "Заведующий кафедры", "зав. каф." },
                    { 4, null, "Программист", "программист" }
                });

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "id_status", "name" },
                values: new object[,]
                {
                    { 1, "Ожидает регистрации" },
                    { 2, "Зарегистрирован" },
                    { 3, "Включен в план работ" },
                    { 4, "В процессе выполнения" },
                    { 5, "Выполнен" },
                    { 6, "Удален" }
                });

            migrationBuilder.InsertData(
                table: "subdivision",
                columns: new[] { "id_subdivision", "creation_date", "id_subordination", "name", "short_name" },
                values: new object[] { 1, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8644), null, "Ученый совет", "" });

            migrationBuilder.InsertData(
                table: "position",
                columns: new[] { "id_position", "id_subordination", "name", "short_name" },
                values: new object[,]
                {
                    { 2, 1, "Старший преподаватель", "ст. преподаватель" },
                    { 3, 1, "Преподаватель", "преподаватель" }
                });

            migrationBuilder.InsertData(
                table: "subdivision",
                columns: new[] { "id_subdivision", "creation_date", "id_subordination", "name", "short_name" },
                values: new object[,]
                {
                    { 2, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8574), 1, "Первый проректор", "" },
                    { 6, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8584), 1, "Проректор по общим вопросам", "" }
                });

            migrationBuilder.InsertData(
                table: "subdivision",
                columns: new[] { "id_subdivision", "creation_date", "id_subordination", "name", "short_name" },
                values: new object[,]
                {
                    { 3, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8646), 2, "Аэрокосмический институт", "АКИ" },
                    { 4, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8647), 2, "Институт менеджмента", "ИМ" },
                    { 5, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8649), 2, "Факультет математики и информационных технологий", "ФМИТ" },
                    { 7, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8586), 6, "Научная библиотека", "" },
                    { 8, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8651), 6, "Центр информационных технологий", "ЦИТ" }
                });

            migrationBuilder.InsertData(
                table: "subdivision",
                columns: new[] { "id_subdivision", "creation_date", "id_subordination", "name", "short_name" },
                values: new object[,]
                {
                    { 9, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8587), 7, "Отдел информационно-библиотечного обслуживания", "" },
                    { 11, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8590), 8, "Отдел ремонта и обслуживания информационно-вычислительной техники", "" },
                    { 12, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8592), 8, "Отдел информационных систем", "" },
                    { 13, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8593), 8, "Отдел цифровых образовательных платформ", "" },
                    { 14, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8595), 8, "Отдел телекоммуникаций", "" },
                    { 15, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8596), 8, "Отдел информационной безопасности", "" },
                    { 26, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8652), 5, "Кафедра программного обеспечения вычислительной техники и автоматизированных систем", "кафедра ПОВТАС" },
                    { 27, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8654), 5, "Кафедра информатики", "" },
                    { 28, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8655), 5, "Кафедра прикладной математики", "кафедра ПМат" },
                    { 29, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8657), 5, "Кафедра вычислительной техники и защиты информации", "кафедра ВТиЗИ" },
                    { 30, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8658), 5, "Кафедра геометрии и компьютерных наук", "кафедра ГКН" },
                    { 31, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8660), 5, "Кафедра компьютерной безопасности и математического обеспечения информационных систем", "кафедра КБМОИС" }
                });

            migrationBuilder.InsertData(
                table: "subdivision",
                columns: new[] { "id_subdivision", "creation_date", "id_subordination", "name", "short_name" },
                values: new object[,]
                {
                    { 10, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8589), 9, "Сектор регистрации пользователей и сервисных услуг", "" },
                    { 16, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8598), 14, "Сектор телекоммуникационных сетей и интернет", "" },
                    { 17, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8600), 14, "Сектор телефонии и охранной сигнализации", "" },
                    { 19, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8603), 13, "Сектор поддержки мультимедийных комплексов", "" },
                    { 20, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8604), 13, "Сектор программно-технической поддержки цифровых образовательных платформ", "" },
                    { 21, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8606), 13, "Сектор сопровождения цифровых образовательных ресурсов", "" },
                    { 22, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8608), 12, "Сектор автоматизированной поддержки организации учебного процесса", "" },
                    { 23, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8609), 12, "Сектор сопровождения программных систем", "" },
                    { 24, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8611), 12, "Сектор систем баз данных", "" },
                    { 25, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8638), 12, "Сектор разработки и сопровождения сайтов", "" },
                    { 32, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8643), 26, "Филиал кафедры ПОВТАС", "" },
                    { 33, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8639), 26, "Учебно-научная лаборатория \"Компьютерное моделирование\"", "" },
                    { 34, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8641), 28, "Филиал кафедры прикладной математики", "" }
                });

            migrationBuilder.InsertData(
                table: "subdivision",
                columns: new[] { "id_subdivision", "creation_date", "id_subordination", "name", "short_name" },
                values: new object[] { 18, new DateTime(2023, 5, 19, 16, 17, 34, 896, DateTimeKind.Local).AddTicks(8601), 17, "Комиссия по обучению безопасным приемам работы и проверки знаний требований охраны труда работников отдела", "" });

            migrationBuilder.CreateIndex(
                name: "fki_app_user_asp_id_fkey",
                table: "app_user",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "AspNetDeviceCode",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "AspNetDeviceCode",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fki_document_id_receiver_fkey",
                table: "document",
                column: "id_receiver");

            migrationBuilder.CreateIndex(
                name: "fki_rtewte",
                table: "document",
                column: "id_document_type");

            migrationBuilder.CreateIndex(
                name: "IX_document_id_sender",
                table: "document",
                column: "id_sender");

            migrationBuilder.CreateIndex(
                name: "fki_document_content_id_document_fkey",
                table: "document_content",
                column: "id_document");

            migrationBuilder.CreateIndex(
                name: "fki_document_status_id_user_fkey",
                table: "document_status",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_document_status_id_document",
                table: "document_status",
                column: "id_document");

            migrationBuilder.CreateIndex(
                name: "IX_document_status_id_status",
                table: "document_status",
                column: "id_status");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "fki_position_id_subordination_fkey",
                table: "position",
                column: "id_subordination");

            migrationBuilder.CreateIndex(
                name: "fki_subdivision_id_subordination_fkey",
                table: "subdivision",
                column: "id_subordination");

            migrationBuilder.CreateIndex(
                name: "fki_user_position_id_position_fkey",
                table: "user_position",
                column: "id_position");

            migrationBuilder.CreateIndex(
                name: "fki_user_position_id_user_fkey",
                table: "user_position",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "fki_user_subdivision_id_subdivision_fkey",
                table: "user_subdivision",
                column: "id_subdivision");

            migrationBuilder.CreateIndex(
                name: "fki_user_subdivision_id_user_fkey",
                table: "user_subdivision",
                column: "id_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowedSubdivisions");

            migrationBuilder.DropTable(
                name: "AspNetDeviceCode");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "document_content");

            migrationBuilder.DropTable(
                name: "document_status");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "user_position");

            migrationBuilder.DropTable(
                name: "user_subdivision");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "position");

            migrationBuilder.DropTable(
                name: "subdivision");

            migrationBuilder.DropTable(
                name: "document_type");

            migrationBuilder.DropTable(
                name: "app_user");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
