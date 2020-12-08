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
                <v-flex xs12 sm12 md12>
                  <v-autocomplete
                  v-model="editedItem.idUser"
                    :items="users"
                    color="white"
                    item-text="name"
                    item-value="id"
                    label="Usuário"
                ></v-autocomplete>
                </v-flex>
                <v-flex xs12 sm12 md12>
                  <v-autocomplete
                  v-model="editedItem.idGame"
                    :items="gamers"
                    color="white"
                    item-text="name"
                    item-value="id"
                    label="Jogo"
                ></v-autocomplete>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-layout row wrap class="dark--text">
                    <v-flex xs6>Devolvido</v-flex>
                    <v-checkbox v-model="editedItem.returned"></v-checkbox>
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
    <v-data-table :headers="headers" :items="loanGames" class="elevation-1">
      <template slot="items" slot-scope="props">
        <td>{{ props.item.id }}</td>
        <td class="text-xs-left">{{ props.item.idUser }}</td>
        <td class="text-xs-left">{{ props.item.user }}</td>
        <td class="text-xs-right">{{ props.item.idGame }}</td>
        <td class="text-xs-left">{{ props.item.game }}</td>
        <td class="text-xs-right">
          <v-checkbox
            v-model="props.item.returned"
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
    headers: [
      { text: "Id", value: "id" },
      { text: "Id Usuário", value: "idUser" },
      {
        text: "Usuário",
        align: "left",
        sortable: false,
        value: "user",
      },
      { text: "Id Jogo", value: "idGame" },
      {
        text: "Jogo",
        align: "left",
        sortable: false,
        value: "game",
      },
      { text: "Devolvido", value: "returned" },
      { text: "Ações", value: "name", sortable: false },
    ],
    loanGames: [],
    editedIndex: -1,
    editedItem: {
      id: null,
      idUser: null,
      user: "",
      idGame: null,
      game: "",
      gameType: "",
      returned: false,
    },
    defaultItem: {
      id: null,
      idUser: null,
      user: "",
      idGame: null,
      game: "",
      gameType: "",
      returned: false,
    },
    users: [],
    gamers: []
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1
        ? "Novo Empréstimo Jogo"
        : "Modificar Empréstimo Jogo";
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
    ...mapActions(["getAPILoanGamers", "getAPIGamers", "getUsers"]),
    initialize() {
      this.getAPILoanGamers()
        .then((response) => {
          this.loanGames = response;
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
              message = "Não foi possível obter os empréstimos dos jogos.";
            }

            this.showAlert("error", message);
          }
        });

        this.getUsers()
        .then((response) => {
          this.users = response;
        })
        .catch((err) => {
          // eslint-disable-next-line
          console.log(err);
        });

        this.getAPIGamers()
        .then((response) => {
          this.gamers = response;
        })
        .catch((err) => {
          // eslint-disable-next-line
          console.log(err);
        });
    },

    showAlert(type, message) {
      this.alert = true;
      this.alertType = type;
      this.alertMessage = message;
    },

    editItem(item) {
      this.editedIndex = this.loanGames.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      const index = this.loanGames.indexOf(item);
      confirm("Are you sure you want to delete this item?") &&
        this.loanGames.splice(index, 1);
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
        Object.assign(this.loanGames[this.editedIndex], this.editedItem);
        
        var loanGame = {
          id: this.editedItem.id,
          idUser: this.editedItem.idUser,
          idGame: this.editedItem.idGame,
          returned: this.editedItem.returned,
        };

        this.$store
          .dispatch("loanGameUpdate", loanGame)
          .then(() => {
            //this.loanGames.push(this.editedItem);
            this.initialize();

            this.showAlert("success", "Empréstimo de jogo modificado.");
          })
          .catch((error) => {
            // eslint-disable-next-line
            //console.log(error.data.message);
            var message = error.data.message
              ? error.data.message
              : "Não foi possível alterar o empréstimo do jogo.";
            this.showAlert("error", message);
          });
      } else {
        var lgRegister = {
          idGame: this.editedItem.idGame,
          idUser: this.editedItem.idUser,
        };
        this.$store
          .dispatch("loanGameRegister", lgRegister)
          .then(() => {
            //this.loanGames.push(this.editedItem);
            this.initialize();

            this.showAlert("success", "Empréstimo de jogo registrado.");
          })
          .catch((error) => {
            // eslint-disable-next-line
            //console.log(error.data.message);
            var message = error.data.message
              ? error.data.message
              : "Não foi possível alterar o empréstimo do jogo.";
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