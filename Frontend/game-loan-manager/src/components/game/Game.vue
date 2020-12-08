<template>
  <div>
    <v-alert v-model="alert" dismissible :type="alertType" class="alertMessage">
      {{ alertMessage }}
    </v-alert>
    <v-toolbar flat color="white">
      <v-toolbar-title>Jogo</v-toolbar-title>
      <v-divider class="mx-2" inset vertical></v-divider>
      <v-spacer></v-spacer>
      <v-btn color="primary" dark class="mb-2" @click="dialog = true"
        >Novo</v-btn
      >
      <v-dialog v-model="dialog" max-width="500px">
        <v-card>
          <v-card-title>
            <span class="headline">{{ formTitle }}</span>
          </v-card-title>

          <v-card-text>
            <v-container grid-list-md>
              <v-layout wrap>
                <v-flex xs12 sm6 md4 v-if="editedItem.id">
                  <v-text-field
                    v-model="editedItem.id"
                    label="ID"
                    :disabled="true"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field
                    v-model="editedItem.name"
                    label="Nome"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-select
                    :items="gameTypes"
                    label="Tipo"
                    v-model="editedItem.type"
                  ></v-select>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field
                    v-model="editedItem.description"
                    label="Descrição"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-layout row wrap class="dark--text">
                    <v-flex xs6>Disponível</v-flex>
                    <v-checkbox v-model="editedItem.available"></v-checkbox>
                  </v-layout>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" flat @click="save">Salvar</v-btn>
            <v-btn color="red darken-1" flat @click="close">Fechar</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-toolbar>
    <v-data-table :headers="headers" :items="jogos" class="elevation-1">
      <template slot="items" slot-scope="props">
        <td>{{ props.item.id }}</td>
        <td class="text-xs-left">{{ props.item.name }}</td>
        <td class="text-xs-left">{{ props.item.type }}</td>
        <td class="text-xs-left">{{ props.item.description }}</td>
        <td class="text-xs-right">
          <v-checkbox
            v-model="props.item.available"
            :disabled="true"
            class="marginCheckBox"
          ></v-checkbox>
        </td>
        <td class="justify-center layout px-0">
          <v-icon small class="mr-2" @click="editItem(props.item)">
            edit
          </v-icon>
          <!--<v-icon small @click="deleteItem(props.item)"> delete </v-icon>-->
        </td>
      </template>
      <template v-slot:no-data>
        <v-btn color="primary" @click="initialize">Reset</v-btn>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  data: () => ({
    dialog: false,
    alert: false,
    alertType: "success",
    alertMessage: "",
    gameTypes: ["DVD", "Tabuleiro"],
    headers: [
      { text: "Id", value: "id" },
      {
        text: "Nome",
        align: "left",
        sortable: true,
        value: "name",
      },
      { text: "Tipo", value: "type" },
      { text: "Descrição", value: "description" },
      { text: "Disponível", value: "available" },
      { text: "Ações", value: "name", sortable: false },
    ],
    jogos: [],
    editedIndex: -1,
    editedItem: {
      id: null,
      name: "",
      type: "",
      description: "",
      available: false,
    },
    defaultItem: {
      id: null,
      name: "",
      type: "",
      description: "",
      available: true,
    },
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Novo Jogo" : "Modificar Jogo";
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
  },

  created() {
    this.initialize();
  },

  methods: {
    ...mapActions(["getAPIGamers"]),
    initialize() {
      this.getAPIGamers()
        .then((response) => {
          this.jogos = response;
        })
        .catch((err) => {
          if (err.status == 401) {
            this.$store.dispatch("logout");
            this.showAlert("error", "Acesso expirado, faça login novamente.");
          } else {
            var message;
            try {
              message = err.data.message;
            } catch {
              message = "Não foi possível obter os jogos.";
            }

            this.showAlert("error", message);
          }
        });
    },

    showAlert(type, message) {
      this.alert = true;
      this.alertType = type;
      this.alertMessage = message;
    },

    editItem(item) {
      this.editedIndex = this.jogos.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      const index = this.jogos.indexOf(item);
      confirm("Are you sure you want to delete this item?") &&
        this.jogos.splice(index, 1);
    },

    close() {
      this.dialog = false;
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      }, 300);
    },

    save() {
      if (this.editedIndex > -1) {
        Object.assign(this.jogos[this.editedIndex], this.editedItem);
        this.$store
          .dispatch("gameUpdate", this.editedItem)
          .then(() => {
            this.initialize();
            this.jogos.push(this.editedItem);

            this.showAlert("success", "Jogo modificado.");
          })
          .catch((error) => {
            // eslint-disable-next-line
            //console.log(error.data.message);
            var message = error.data.message
              ? error.data.message
              : "Não foi possível alterar o jogo.";
            this.showAlert("error", message);
          });
      } else {
        this.$store
          .dispatch("gameRegister", this.editedItem)
          .then(() => {
            this.initialize();
            this.jogos.push(this.editedItem);

            this.showAlert("success", "Jogo registrado.");
          })
          .catch((error) => {
            // eslint-disable-next-line
            //console.log(error.data.message);
            var message = error.data.message
              ? error.data.message
              : "Não foi possível inserir o jogo.";
            this.showAlert("error", message);
          });
      }

      setTimeout(() => {
        this.alert = false;
      }, 18000);

      this.close();
    },
  },
};
</script>

<style>
.alertMessage {
  position: fixed;
  top: 30px;
  left: 30%;
  z-index: 100;
  width: 30%;
}
.v-datatable .v-input--selection-controls .marginCheckBox {
  margin: 3px 0 -5px;
}
</style>