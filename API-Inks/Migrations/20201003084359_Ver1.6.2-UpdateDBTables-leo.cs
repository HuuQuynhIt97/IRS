using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver162UpdateDBTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ArtProcesses_ArtProcessID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ArticleNos_ArticleNoID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ModelNames_ModelNameID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ModelNos_ModelNoID",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "Abnormals");

            migrationBuilder.DropTable(
                name: "BPFCHistories");

            migrationBuilder.DropTable(
                name: "GlueIngredient");

            migrationBuilder.DropTable(
                name: "IngredientInfoReports");

            migrationBuilder.DropTable(
                name: "IngredientsInfos");

            migrationBuilder.DropTable(
                name: "Line");

            migrationBuilder.DropTable(
                name: "MapModel");

            migrationBuilder.DropTable(
                name: "MixingInfos");

            migrationBuilder.DropTable(
                name: "PartName");

            migrationBuilder.DropTable(
                name: "PartName2");

            migrationBuilder.DropTable(
                name: "PlanDetails");

            migrationBuilder.DropTable(
                name: "Stirs");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Glues");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Kinds");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "MaterialName");

            migrationBuilder.DropTable(
                name: "BPFCEstablishes");

            migrationBuilder.DropTable(
                name: "ArtProcesses");

            migrationBuilder.DropTable(
                name: "ArticleNos");

            migrationBuilder.DropTable(
                name: "ModelNos");

            migrationBuilder.DropTable(
                name: "ModelNames");


        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abnormals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Building = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ingredient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abnormals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BPFCHistories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    After = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BPFCEstablishID = table.Column<int>(type: "int", nullable: false),
                    Before = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GlueID = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPFCHistories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IngredientInfoReports",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IngredientInfoID = table.Column<int>(type: "int", nullable: false),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientInfoReports", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaysToExpiration = table.Column<int>(type: "int", nullable: false),
                    ExpiredTime = table.Column<int>(type: "int", nullable: false),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaterialNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VOC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isShow = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ingredients_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsInfos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsInfos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kinds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kinds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Line",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MapModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModelNameID = table.Column<int>(type: "int", nullable: false),
                    ModelNoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaterialName",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialName", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ModelNames",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelNames", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PartName",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartName", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PartName2",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartName2", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Stirs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GlueName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MixingInfoID = table.Column<int>(type: "int", nullable: false),
                    SettingID = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stirs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ModelNos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelNameID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelNos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ModelNos_ModelNames_ModelNameID",
                        column: x => x.ModelNameID,
                        principalTable: "ModelNames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleNos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModelNoID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleNos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArticleNos_ModelNos_ModelNoID",
                        column: x => x.ModelNoID,
                        principalTable: "ModelNos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtProcesses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleNoID = table.Column<int>(type: "int", nullable: false),
                    ProcessID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtProcesses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArtProcesses_ArticleNos_ArticleNoID",
                        column: x => x.ArticleNoID,
                        principalTable: "ArticleNos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtProcesses_Processes_ProcessID",
                        column: x => x.ProcessID,
                        principalTable: "Processes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BPFCEstablishes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalBy = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatus = table.Column<bool>(type: "bit", nullable: false),
                    ArtProcessID = table.Column<int>(type: "int", nullable: false),
                    ArticleNoID = table.Column<int>(type: "int", nullable: false),
                    BuildingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedStatus = table.Column<bool>(type: "bit", nullable: false),
                    ModelNameID = table.Column<int>(type: "int", nullable: false),
                    ModelNoID = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPFCEstablishes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BPFCEstablishes_ArtProcesses_ArtProcessID",
                        column: x => x.ArtProcessID,
                        principalTable: "ArtProcesses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BPFCEstablishes_ArticleNos_ArticleNoID",
                        column: x => x.ArticleNoID,
                        principalTable: "ArticleNos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BPFCEstablishes_ModelNames_ModelNameID",
                        column: x => x.ModelNameID,
                        principalTable: "ModelNames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BPFCEstablishes_ModelNos_ModelNoID",
                        column: x => x.ModelNoID,
                        principalTable: "ModelNos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Glues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BPFCEstablishID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiredTime = table.Column<int>(type: "int", nullable: false),
                    KindID = table.Column<int>(type: "int", nullable: true),
                    MaterialID = table.Column<int>(type: "int", nullable: true),
                    MaterialNameID = table.Column<int>(type: "int", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartID = table.Column<int>(type: "int", nullable: true),
                    isShow = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Glues_BPFCEstablishes_BPFCEstablishID",
                        column: x => x.BPFCEstablishID,
                        principalTable: "BPFCEstablishes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Glues_Kinds_KindID",
                        column: x => x.KindID,
                        principalTable: "Kinds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Glues_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Glues_MaterialName_MaterialNameID",
                        column: x => x.MaterialNameID,
                        principalTable: "MaterialName",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Glues_Parts_PartID",
                        column: x => x.PartID,
                        principalTable: "Parts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BPFCEstablishID = table.Column<int>(type: "int", nullable: false),
                    BPFCName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourlyOutput = table.Column<int>(type: "int", nullable: false),
                    WorkingHour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Plans_BPFCEstablishes_BPFCEstablishID",
                        column: x => x.BPFCEstablishID,
                        principalTable: "BPFCEstablishes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plans_Buildings_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Buildings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlueIngredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Allow = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GlueID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlueIngredient", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GlueIngredient_Glues_GlueID",
                        column: x => x.GlueID,
                        principalTable: "Glues",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlueIngredient_Ingredients_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MixingInfos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingID = table.Column<int>(type: "int", nullable: false),
                    ChemicalA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChemicalB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChemicalC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChemicalD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChemicalE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GlueID = table.Column<int>(type: "int", nullable: false),
                    GlueName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MixBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MixingInfos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MixingInfos_Glues_GlueID",
                        column: x => x.GlueID,
                        principalTable: "Glues",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BPFCName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumption = table.Column<double>(type: "float", nullable: false),
                    GlueID = table.Column<int>(type: "int", nullable: false),
                    GlueName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanID = table.Column<int>(type: "int", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlanDetails_Plans_PlanID",
                        column: x => x.PlanID,
                        principalTable: "Plans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ArtProcessID",
                table: "Schedules",
                column: "ArtProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ArticleNoID",
                table: "Schedules",
                column: "ArticleNoID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ModelNameID",
                table: "Schedules",
                column: "ModelNameID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ModelNoID",
                table: "Schedules",
                column: "ModelNoID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleNos_ModelNoID",
                table: "ArticleNos",
                column: "ModelNoID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtProcesses_ArticleNoID",
                table: "ArtProcesses",
                column: "ArticleNoID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtProcesses_ProcessID",
                table: "ArtProcesses",
                column: "ProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_BPFCEstablishes_ArtProcessID",
                table: "BPFCEstablishes",
                column: "ArtProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_BPFCEstablishes_ArticleNoID",
                table: "BPFCEstablishes",
                column: "ArticleNoID");

            migrationBuilder.CreateIndex(
                name: "IX_BPFCEstablishes_ModelNameID",
                table: "BPFCEstablishes",
                column: "ModelNameID");

            migrationBuilder.CreateIndex(
                name: "IX_BPFCEstablishes_ModelNoID",
                table: "BPFCEstablishes",
                column: "ModelNoID");

            migrationBuilder.CreateIndex(
                name: "IX_GlueIngredient_GlueID",
                table: "GlueIngredient",
                column: "GlueID");

            migrationBuilder.CreateIndex(
                name: "IX_GlueIngredient_IngredientID",
                table: "GlueIngredient",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_Glues_BPFCEstablishID",
                table: "Glues",
                column: "BPFCEstablishID");

            migrationBuilder.CreateIndex(
                name: "IX_Glues_KindID",
                table: "Glues",
                column: "KindID");

            migrationBuilder.CreateIndex(
                name: "IX_Glues_MaterialID",
                table: "Glues",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_Glues_MaterialNameID",
                table: "Glues",
                column: "MaterialNameID");

            migrationBuilder.CreateIndex(
                name: "IX_Glues_PartID",
                table: "Glues",
                column: "PartID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_SupplierID",
                table: "Ingredients",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_MixingInfos_GlueID",
                table: "MixingInfos",
                column: "GlueID");

            migrationBuilder.CreateIndex(
                name: "IX_ModelNos_ModelNameID",
                table: "ModelNos",
                column: "ModelNameID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanDetails_PlanID",
                table: "PlanDetails",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_BPFCEstablishID",
                table: "Plans",
                column: "BPFCEstablishID");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_BuildingID",
                table: "Plans",
                column: "BuildingID");

         
        }
    }
}
