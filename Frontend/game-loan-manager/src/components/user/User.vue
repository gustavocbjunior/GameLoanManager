<template>
  <div>
    <v-alert v-model="alert" dismissible :type="alertType" class="alertMessage">
      {{ alertMessage }}
    </v-alert>
    <v-toolbar flat color="white">
      <v-toolbar-title>Usuários</v-toolbar-title>
      <v-divider class="mx-2" inset vertical></v-divider>
      <v-spacer></v-spacer>
    </v-toolbar>
    <v-data-table :headers="headers" :items="users" class="elevation-1">
      <template slot="items" slot-scope="props">
        <td>{{ props.item.id }}</td>
        <td class="text-xs-left">{{ props.item.login }}</td>
        <td class="text-xs-left">{{ props.item.name }}</td>
        <td class="text-xs-right">{{ props.item.email }}</td>
        <td class="text-xs-right">{{ props.item.phone }}</td>
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
        text: "login",
        align: "left",
        sortable: true,
        value: "login",
      },
      {
        text: "Nome",
        align: "left",
        sortable: false,
        value: "name",
      },
      {
        text: "E-mail",
        align: "left",
        sortable: true,
        value: "email",
      },
      { text: "Telefone", value: "phone" },
      { text: "Ações", value: "id", sortable: false },
    ],
    users: [],
    editedIndex: -1,
    editedItem: {
      id: null,
      login: "",
      name: "",
      email: "",
      phone: ""
    },
    defaultItem: {
      id: null,
      login: "",
      name: "",
      email: "",
      phone: ""
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
    ...mapActions(["getUsers"]),
    initialize() {
      this.getUsers()
        .then((response) => {
          this.users = response;
        })
        .catch((err) => {
          if (err.status == 401) {
            this.$store.dispatch("logout");
            this.showAlert("error", "Acesso expirado, faça login novamente.");
          } else {
            this.showAlert("error", err.data.message);
          }
        });
    },

    showAlert(type, message) {
      this.alert = true;
      this.alertType = type;
      this.alertMessage = message;
    },

    editItem(item) {
      this.editedIndex = this.users.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      const index = this.users.indexOf(item);
      confirm("Are you sure you want to delete this item?") &&
        this.users.splice(index, 1);
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
        Object.assign(this.users[this.editedIndex], this.editedItem);
        this.$store
          .dispatch("gameUpdate", this.editedItem)
          .then(() => {
            this.initialize();
            this.users.push(this.editedItem);

            this.showAlert("success", "Jogo modificado.");
          })
          .catch((error) => {
            // eslint-disable-next-line
            //console.log(error.data.message);
            this.showAlert("error", error.data.message);
          });
      } else {
        this.$store
          .dispatch("gameRegister", this.editedItem)
          .then(() => {
            this.initialize();
            this.users.push(this.editedItem);

            this.showAlert("success", "Jogo registrado.");
          })
          .catch((error) => {
            // eslint-disable-next-line
            //console.log(error.data.message);
            this.showAlert("error", error.data.message);
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